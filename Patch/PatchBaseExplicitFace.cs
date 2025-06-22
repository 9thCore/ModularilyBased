using HarmonyLib;
using ModularilyBased.Library;
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

        public static void PatchVertical(BaseExplicitFace face)
        {
            if (!face.IsBottomFace())
            {
                return;
            }

            if (face.IsCenterFace(out int centerIndex))
            {
                PatchCenter(face, centerIndex);
            }
            else
            {
                PatchLadder(face);
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

        public static void PatchCenter(BaseExplicitFace face, int centerIndex)
        {
            if (!TryCreateCollider(face, out GameObject obj))
            {
                return;
            }

            BaseFaceIdentifier identifier = face.GetComponent<BaseFaceIdentifier>();
            identifier.CenterFaceIndex = centerIndex;
        }

        public static void PatchLadder(BaseExplicitFace face)
        {
            if (!TryCreateCollider(face, out GameObject obj))
            {
                return;
            }
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
            identifier.Room = type;
            identifier.Face = faceType;

            result = Plugin.createSnapAsPrimitive ? GameObject.CreatePrimitive(PrimitiveType.Cube) : new GameObject();
            identifier.Collider = result.EnsureComponent<BoxCollider>();

            result.layer = LayerID.Trigger;
            identifier.Collider.isTrigger = true;

            result.transform.SetParent(face.transform, false);
            result.transform.localScale = scale;
            result.transform.localRotation = rotation;
            result.transform.localPosition = Vector3.zero;

            return true;
        }
    }
}
