using HarmonyLib;
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
            if (!__result.face.HasValue
                || __result.TryGetComponent(out BaseFaceIdentifier _))
            {
                return;
            }

            switch (__result.face.Value.direction)
            {
                case Base.Direction.Above:
                    // PatchVertical(__result);
                    break;
                case Base.Direction.North
                or Base.Direction.South
                or Base.Direction.East
                or Base.Direction.West:
                    PatchHorizontal(__result);
                    break;
                case Base.Direction.Below
                or Base.Direction.Count:
                    return;
            }
        }

        public static void PatchHorizontal(BaseExplicitFace face)
        {
            if (!face.TryGetCellIdentifier(out TechType type)
                || !face.TryGetFaceType(out BaseFaceIdentifier.FaceType faceType)
                || !face.TryGetColliderDistance(out float distance)
                || !face.TryGetColliderScale(out Vector3 scale))
            {
                return;
            }

            BaseFaceIdentifier identifier = face.gameObject.AddComponent<BaseFaceIdentifier>();
            identifier.room = type;
            identifier.face = faceType;

            GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
            BoxCollider collider = obj.AddComponent<BoxCollider>();

            obj.transform.SetParent(face.transform, false);
            obj.transform.localScale = scale;
            obj.transform.localRotation = Quaternion.identity;
            obj.transform.localPosition = Vector3.zero;
            obj.transform.position += obj.transform.right * distance;

            identifier.collider = collider;
        }
    }
}
