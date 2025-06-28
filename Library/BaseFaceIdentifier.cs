using ModularilyBased.JSON;
using System;
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
        // Only relevant for large rooms, as they contain multiple center faces
        public CenterSnapType CenterType { get; internal set; } = CenterSnapType.None;
        public Base.Face[] SeabaseFaces { get; internal set; }

        public void Link(Base seabase, BaseCell cell, Base.Face[] faces)
        {
            seabase.onPostRebuildGeometry += UpdateFace;
            this.seabase = seabase;
            this.cell = cell;
            SeabaseFaces = new Base.Face[faces.Length];
            Array.Copy(faces, SeabaseFaces, faces.Length);
            UpdateFace(seabase);
        }

        public void Link(Base seabase, BaseCell cell, Base.Face face)
        {
            Link(seabase, cell, new Base.Face[] { face });
        }

        public void OnDestroy()
        {
            if (seabase == null)
            {
                return;
            }

            seabase.onPostRebuildGeometry -= UpdateFace;
        }

        public void UpdateFace(Base seabase)
        {
            bool allFacesValid = SeabaseFaces.All(face =>
            {
                Base.Face shiftedFace = new(face.cell + cell.cell, face.direction);
                Base.FaceType type = seabase.GetFace(shiftedFace);
                return ExistingFace(type);
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

        // To differentiate between each snap type, as larger modules in the large room may need a different snap point.
        public enum CenterSnapType
        {
            None,
            OneFace,
            TwoFaces,
            ThreeFaces,
            FourFaces
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
            identifier.CenterType = data.centerSnapType;

            if (data.face == FaceType.Center)
            {
                identifier.CenterType = CenterSnapType.OneFace;
            }

            GameObject collider = Plugin.debugMode.Value ? GameObject.CreatePrimitive(PrimitiveType.Cube) : new GameObject();
            identifier.Collider = collider.EnsureComponent<BoxCollider>();

            collider.layer = LayerID.Trigger;
            identifier.Collider.isTrigger = true;

            collider.transform.SetParent(go.transform, false);
            collider.transform.localScale = data.scale;
            collider.transform.localRotation = Quaternion.identity;
            collider.transform.localPosition = Vector3.zero;
        }

        public Base.Face[] CloneFaces(Base.Face[] faces)
        {
            Base.Face[] clones = new Base.Face[faces.Length];
            for (int i = 0; i < faces.Length; i++)
            {
                clones[i] = faces[i];
            }
            return clones;
        }
    }
}
