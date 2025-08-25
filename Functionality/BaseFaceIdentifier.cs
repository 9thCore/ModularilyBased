using ModularilyBased.API.Buildable.PlaceRule;
using ModularilyBased.API.Register;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ModularilyBased.Functionality
{
    internal class BaseFaceIdentifier : MonoBehaviour
    {
        private BaseCell cell;
        private SnapHolder pointer;

        public TechType Room = TechType.None;
        public FaceType Face;
        public BoxCollider Collider;
        public Base.Face[] SeabaseFaces;
        public int RequiredModuleSize;

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

        public FaceType GetSnapType()
        {
            return Face;
        }

        internal static void CreateSnap(FaceData data, TechType room, Transform parent, out BaseFaceIdentifier identifier)
        {
            GameObject go = new GameObject();
            go.transform.SetParent(parent);
            go.transform.localPosition = data.Position;
            go.transform.localRotation = data.Rotation;

            identifier = go.AddComponent<BaseFaceIdentifier>();
            identifier.Room = room;
            identifier.Face = data.FaceType;
            identifier.RequiredModuleSize = data.RequiredModuleSize;

            GameObject collider = new GameObject();
            identifier.Collider = collider.EnsureComponent<BoxCollider>();

            collider.layer = LayerID.Trigger;
            identifier.Collider.isTrigger = true;

            collider.transform.SetParent(go.transform, false);
            collider.transform.localScale = data.Scale;
            collider.transform.localRotation = Quaternion.identity;
            collider.transform.localPosition = Vector3.zero;

            go.name = $"SnapPoint_{room}/{data.FaceType}";
            collider.name = $"SnapPointTrigger_{room}/{data.FaceType}";
        }
    }
}
