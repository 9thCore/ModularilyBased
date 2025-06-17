using HarmonyLib;
using System;
using UnityEngine;

namespace ModularilyBased.Patch
{
    [HarmonyPatch]
    public class PatchBaseExplicitFace
    {
        [HarmonyPatch(typeof(BaseExplicitFace), nameof(BaseExplicitFace.MakeFaceDeconstructable))]
        [HarmonyPostfix]
        public static void Patch(BaseExplicitFace __result)
        {
            if (__result == null
                || !__result.face.HasValue
                || __result.TryGetComponent(out BaseFaceIdentifier _))
            {
                return;
            }

            switch (__result.face.Value.direction)
            {
                case Base.Direction.Below:
                    PatchVertical(__result);
                    break;
                case Base.Direction.North
                or Base.Direction.South
                or Base.Direction.East
                or Base.Direction.West:
                    PatchHorizontal(__result);
                    break;
                default:
                    return;
            }
        }

        public static void PatchHorizontal(BaseExplicitFace face)
        {
            if (!TryCreateCollider(face, out GameObject obj)
                || !face.TryGetColliderDistance(out float distance))
            {
                return;
            }

            obj.transform.position += obj.transform.right * distance;
        }

        public static void PatchVertical(BaseExplicitFace face)
        {
            Vector3 position = face.transform.localPosition;

            // eugh
            if (!face.gameObject.name.Contains("Cover")
                || Math.Abs(position.x - 5f) > 0.1f
                || (position.z % 5f) > 0.1f)
            {
                return;
            }

            if (!face.TryGetCellIdentifier(out TechType type)
                || !face.TryGetFaceType(out BaseFaceIdentifier.FaceType faceType)
                || !face.TryGetColliderDistance(out float distance)
                || !face.TryGetColliderScale(out Vector3 scale)
                || !face.TryGetColliderRotationOffset(out Quaternion rotation))
            {
                return;
            }

            face.gameObject.SetActive(false);
        }

        public static bool TryCreateCollider(BaseExplicitFace face, out GameObject result)
        {
            if (!face.TryGetCellIdentifier(out TechType type)
                || !face.TryGetFaceType(out BaseFaceIdentifier.FaceType faceType)
                || !face.TryGetColliderScale(out Vector3 scale)
                || !face.TryGetColliderRotationOffset(out Quaternion rotation))
            {
                result = null;
                return false;
            }

            BaseFaceIdentifier identifier = face.gameObject.AddComponent<BaseFaceIdentifier>();
            identifier.room = type;
            identifier.face = faceType;

            result = new GameObject();
            BoxCollider collider = result.EnsureComponent<BoxCollider>();

            result.layer = LayerID.Trigger;
            collider.isTrigger = true;

            result.transform.SetParent(face.transform, false);
            result.transform.localScale = scale;
            result.transform.localRotation = rotation;
            result.transform.localPosition = Vector3.zero;

            identifier.collider = collider;
            identifier.explicitFace = face;
            identifier.seabase = face.gameObject.GetComponentInParent<Base>();

            return true;
        }
    }
}
