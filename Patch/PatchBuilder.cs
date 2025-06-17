using HarmonyLib;
using ModularilyBased.Library;
using ModularilyBased.Library.TransformRule.Position;
using ModularilyBased.Library.TransformRule.Rotation;
using UnityEngine;

namespace ModularilyBased.Patch
{
    [HarmonyPatch]
    public static class PatchBuilder
    {
        // awful
        public static int lastPlaceableTime = 0;

        [HarmonyPatch(typeof(Builder), nameof(Builder.SetPlaceOnSurface))]
        [HarmonyPostfix]
        public static void PatchPlacement()
        {
            GameObject prefab = Builder.prefab;
            if (!prefab.TryGetComponent(out ModuleSnapper snapper)
                || !TryFindMatchingModuleCollider(out BaseFaceIdentifier identifier))
            {
                return;
            }

            lastPlaceableTime = Time.frameCount;

            Transform transform = identifier.collider.transform;

            PositionRule positionRule = snapper.transformationRule.positionRule;
            RotationRule rotationRule = snapper.transformationRule.rotationRule;

            Builder.placePosition = positionRule.Calculate(transform);
            Builder.placeRotation = rotationRule.Calculate(transform);

            return;
        }

        [HarmonyPatch(typeof(Builder), nameof(Builder.UpdateAllowed))]
        [HarmonyPostfix]
        public static void PatchUpdateAllowed(ref bool __result)
        {
            GameObject prefab = Builder.prefab;
            if (!prefab.TryGetComponent(out ModuleSnapper snapper))
            {
                return;
            }

            __result = (lastPlaceableTime == Time.frameCount);
        }

        public static bool TryFindMatchingModuleCollider(out BaseFaceIdentifier result)
        {
            GameObject prefab = Builder.prefab;
            if (prefab == null
                || !prefab.TryGetComponent(out ModuleSnapper snapper))
            {
                result = null;
                return false;
            }

            float closestNonIdentifierDistance = float.PositiveInfinity;

            BaseFaceIdentifier closestIdentifier = null;
            float closestIdentifierDistance = float.PositiveInfinity;

            Transform transform = Builder.GetAimTransform();
            Vector3 forward = transform.TransformDirection(Vector3.forward);

            int layerMask = Builder.placeLayerMask.value | (1 << LayerID.Trigger);

            RaycastHit[] hits = Physics.RaycastAll(transform.position, forward, Builder.placeMaxDistance, layerMask, QueryTriggerInteraction.Collide);
            foreach (RaycastHit hit in hits)
            {
                BaseFaceIdentifier faceIdentifier = hit.transform.GetComponentInParent<BaseFaceIdentifier>();
                float distance = hit.distance;

                if (faceIdentifier == null)
                {
                    if (distance < closestNonIdentifierDistance)
                    {
                        closestNonIdentifierDistance = distance;
                    }

                    continue;
                }

                if (snapper.CanBuildOn(faceIdentifier))
                {
                    if (distance < closestIdentifierDistance)
                    {
                        closestIdentifierDistance = distance;
                        closestIdentifier = faceIdentifier;
                    }
                }
            }

            // Don't care about it if it's blocked by something else
            if (closestIdentifierDistance > closestNonIdentifierDistance)
            {
                result = null;
                return false;
            }

            result = closestIdentifier;
            return (closestIdentifier != null);
        }
    }
}
