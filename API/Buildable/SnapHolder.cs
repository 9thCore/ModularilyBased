using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ModularilyBased.API.Buildable
{
    public class SnapHolder : MonoBehaviour
    {
        public GameObject root;

        public delegate void OnGeometryUpdate(Base seabase, HashSet<Int3> occupiedCells);
        public event OnGeometryUpdate OnFaceUpdates;

        public void Link(Base seabase)
        {
            seabase.onPostRebuildGeometry += UpdateFaces;
        }

        public void UpdateFaces(Base seabase)
        {
            IBaseModuleGeometry[] modules = seabase.GetComponentsInChildren<IBaseModuleGeometry>();
            HashSet<Int3> occupiedCells = new();

            modules.Where(module => module is MonoBehaviour behaviour && behaviour.enabled)
                .ForEach(module => occupiedCells.Add(module.geometryFace.cell));

            OnFaceUpdates?.Invoke(seabase, occupiedCells);
        }

        public void SetSibling(Transform sibling)
        {
            root = new GameObject();
            root.transform.SetParent(sibling.parent);
            root.transform.localPosition = sibling.localPosition;
            root.transform.localRotation = sibling.localRotation;
            root.name = $"{PluginInfo.PLUGIN_NAME} Snap Root";
        }

        public void OnDestroy()
        {
            DestroyImmediate(root);
        }
    }
}
