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
        private static int lastPlaceableTime = 0;

        [HarmonyPatch(typeof(Builder), nameof(Builder.CheckAsSubModule))]
        [HarmonyPostfix]
        public static void PatchPlacement(ref bool __result, ref Collider hitCollider)
        {
            GameObject prefab = Builder.prefab;
            if (Player.main.currentSub == null
                || !prefab.TryGetComponent(out ModuleSnapper snapper)
                || !TryFindMatchingModuleCollider(Builder.prefab, out BaseFaceIdentifier identifier))
            {
                return;
            }

            lastPlaceableTime = Time.frameCount;

            Transform transform = identifier.Collider.transform;

            PositionRule positionRule = snapper.transformationRule.PositionRule;
            RotationRule rotationRule = snapper.transformationRule.RotationRule;

            Builder.placePosition = positionRule.Calculate(transform);
            Builder.placeRotation = rotationRule.Calculate(transform);

            hitCollider = null;
            __result = true;
        }

        [HarmonyPatch(typeof(Builder), nameof(Builder.UpdateAllowed))]
        [HarmonyPostfix]
        public static void PatchUpdateAllowed(ref bool __result)
        {
            GameObject prefab = Builder.prefab;
            if (!prefab.TryGetComponent(out ModuleSnapper _))
            {
                return;
            }

            __result = __result && (lastPlaceableTime == Time.frameCount);
        }

        // This method is very heavy - don't use it more than required and preferably not at all
        public static bool TryFindMatchingModuleCollider(GameObject prefab, out BaseFaceIdentifier result)
        {
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
