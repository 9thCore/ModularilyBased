using ModularilyBased.JSON;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ModularilyBased.Library
{
    public class BaseFaceIdentifier : MonoBehaviour
    {
        private Base seabase;
        private BaseCell cell;

        public TechType Room { get; internal set; } = TechType.None;
        public FaceType Face { get; internal set; } = FaceType.None;
        public BoxCollider Collider { get; internal set; }
        public Base.Face[] SeabaseFaces { get; internal set; }

        public void Link(Base seabase, BaseCell cell, Base.Face[] faces, SnapHolder pointer)
        {
            this.seabase = seabase;
            this.cell = cell;
            SeabaseFaces = new Base.Face[faces.Length];
            Array.Copy(faces, SeabaseFaces, faces.Length);
            pointer.OnFaceUpdates += UpdateFace;
        }

        public void Link(Base seabase, BaseCell cell, Base.Face face, SnapHolder pointer)
        {
            Link(seabase, cell, new Base.Face[] { face }, pointer);
        }

        public void OnDestroy()
        {
            if (seabase == null)
            {
                return;
            }
        }

        public void UpdateFace(Base seabase, HashSet<Int3> occupiedCells)
        {
            bool allFacesValid = SeabaseFaces.All(face =>
            {
                Base.Face shiftedFace = new(face.cell + cell.cell, face.direction);
                Base.FaceType type = seabase.GetFace(shiftedFace);
                return ExistingFace(type) && !occupiedCells.Contains(shiftedFace.cell);
            });
            
            Collider.gameObject.SetActive(allFacesValid);
        }

        public static bool ExistingFace(Base.FaceType type)
        {
            return type == Base.FaceType.Solid;
        }

        public bool IsWall()
        {
            return Face == FaceType.LongSide || Face == FaceType.ShortSide || Face == FaceType.CorridorSide;
        }

        public bool IsCenter()
        {
            return Face == FaceType.Center;
        }

        public bool IsCap()
        {
            return Face == FaceType.CorridorCap;
        }

        public bool IsLadder()
        {
            return Face == FaceType.Ladder;
        }

        public enum FaceType
        {
            None,
            LongSide,
            ShortSide,
            CorridorSide,
            CorridorCap,
            // Top,
            // Bottom,
            Center,
            Ladder,
            WaterPark
        }

        public static void CreateSnap(FaceData data, TechType room, Transform parent, out BaseFaceIdentifier identifier)
        {
            GameObject go = new GameObject();
            go.transform.SetParent(parent);
            go.transform.localPosition = data.position;
            go.transform.localRotation = data.rotation;

            identifier = go.AddComponent<BaseFaceIdentifier>();
            identifier.Room = room;
            identifier.Face = data.face;

            GameObject collider = Plugin.debugMode.Value ? GameObject.CreatePrimitive(PrimitiveType.Cube) : new GameObject();
            identifier.Collider = collider.EnsureComponent<BoxCollider>();

            collider.layer = LayerID.Trigger;
            identifier.Collider.isTrigger = true;

            collider.transform.SetParent(go.transform, false);
            collider.transform.localScale = data.scale;
            collider.transform.localRotation = Quaternion.identity;
            collider.transform.localPosition = Vector3.zero;

            go.name = $"SnapPoint_{room}/{data.face}";
            collider.name = $"SnapPointTrigger_{room}/{data.face}";
        }
    }
}
