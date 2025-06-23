using ModularilyBased.JSON;
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
        public int CenterFaceIndex { get; internal set; }
        public Base.Face SeabaseFace { get; internal set; }

        public void Link(Base seabase, BaseCell cell, Base.Face face)
        {
            seabase.onPostRebuildGeometry += UpdateFace;
            this.seabase = seabase;
            this.cell = cell;
            SeabaseFace = face;
            UpdateFace(seabase);
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
            Base.Face shiftedFace = new(SeabaseFace.cell + cell.cell, SeabaseFace.direction);
            Base.FaceType type = seabase.GetFace(shiftedFace);
            Collider.gameObject.SetActive(ExistingFace(type));
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
            Ladder
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
            identifier.CenterFaceIndex = data.centerFaceIndex;

            GameObject collider = Plugin.createSnapAsPrimitive ? GameObject.CreatePrimitive(PrimitiveType.Cube) : new GameObject();
            identifier.Collider = collider.EnsureComponent<BoxCollider>();

            collider.layer = LayerID.Trigger;
            identifier.Collider.isTrigger = true;

            collider.transform.SetParent(go.transform, false);
            collider.transform.localScale = data.scale;
            collider.transform.localRotation = data.colliderRotation;
            collider.transform.localPosition = data.colliderPosition;
        }
    }
}
