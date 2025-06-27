using UnityEngine;

namespace ModularilyBased.Library
{
    public class SnapHolder : MonoBehaviour
    {
        public GameObject root;

        public void SetSibling(Transform sibling)
        {
            root = new GameObject();
            root.transform.SetParent(sibling.parent);
            root.transform.localPosition = sibling.localPosition;
            root.transform.localRotation = sibling.localRotation;
        }

        public void OnDestroy()
        {
            Destroy(root);
        }
    }
}
