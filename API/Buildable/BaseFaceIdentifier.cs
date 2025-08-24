using ModularilyBased.API.Buildable.PlaceRule;
using ModularilyBased.JSON;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ModularilyBased.API.Buildable
{
    public class BaseFaceIdentifier : MonoBehaviour
    {
        private BaseCell cell;
        private SnapHolder pointer;

        public TechType Room { get; internal set; } = TechType.None;
        public FaceType Face { get; internal set; } = FaceType.None;
        public BoxCollider Collider { get; internal set; }
        public Base.Face[] SeabaseFaces { get; internal set; }

        internal void Link(Base seabase, BaseCell cell, Base.Face[] faces, SnapHolder pointer)
        {
            this.cell = cell;
            this.pointer = pointer;
            SeabaseFaces = new Base.Face[faces.Length];
            Array.Copy(faces, SeabaseFaces, faces.Length);
            pointer.OnFaceUpdates += UpdateFace;
        }

        private void UpdateFace(Base seabase, HashSet<Int3> occupiedCells)
        {
            bool allFacesValid = SeabaseFaces.All(face =>
            {
                Base.Face shiftedFace = new(face.cell + cell.cell, face.direction);
                Base.FaceType type = seabase.GetFace(shiftedFace);
                bool flag = ExistingFace(type);
                if (Face != FaceType.WaterPark)
                {
                    flag &= !occupiedCells.Contains(shiftedFace.cell);
                }
                return flag;
            });
            
            Collider.gameObject.SetActive(allFacesValid);
        }

        internal void OnDestroy()
        {
            if (pointer == null)
            {
                return;
            }

            pointer.OnFaceUpdates -= UpdateFace;
        }

        internal static bool ExistingFace(Base.FaceType type)
        {
            return type == Base.FaceType.Solid;
        }

        public PlacementRule.SnapType GetSnapType()
        {
            return Face switch
            {
                FaceType.LongSide or FaceType.ShortSide or FaceType.CorridorSide => PlacementRule.SnapType.Wall,
                FaceType.Center => PlacementRule.SnapType.Center,
                FaceType.CorridorCap => PlacementRule.SnapType.CorridorCap,
                FaceType.Ladder => PlacementRule.SnapType.Ladder,
                FaceType.WaterPark => PlacementRule.SnapType.WaterParkSide,
                _ => PlacementRule.SnapType.None
            };
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

        internal static void CreateSnap(FaceData data, TechType room, Transform parent, out BaseFaceIdentifier identifier)
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
