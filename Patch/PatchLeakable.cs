using HarmonyLib;
using ModularilyBased.JSON;
using ModularilyBased.Library;
using Oculus.Platform.Models;
using Sentry;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UWE;
using static ClipMapManager;

namespace ModularilyBased.Patch
{
    [HarmonyPatch(typeof(Leakable))]
    public static class PatchLeakable
    {
        [HarmonyPatch(nameof(Leakable.Start))]
        [HarmonyPostfix]
        public static void Patch(Leakable __instance)
        {
            if (__instance == null
                || !__instance.TryGetComponent(out BaseCell cell))
            {
                return;
            }

            Base seabase = __instance.GetComponentInParent<Base>();
            if (seabase == null)
            {
                return;
            }

            int index = Array.IndexOf(seabase.cellObjects, cell.transform);
            if (index == -1)
            {
                return;
            }

            Base.CellType type = seabase.GetCell(index);
            string extraID = type.ToString();

            BaseDeconstructable decon = cell.GetComponentInChildren<BaseDeconstructable>();
            TechType room = decon.recipe;
            string filename = room.ToString();

            SnapHolder pointer = cell.gameObject.EnsureComponent<SnapHolder>();
            pointer.Link(seabase);
            pointer.SetSibling(cell.transform);
            GameObject sibling = pointer.root;

            if (!RoomFaceData.TryGetFaces(filename, extraID, out List<FaceData> storage))
            {
                return;
            }

            CoroutineHost.StartCoroutine(CreateSnap(storage, room, sibling.transform, seabase, cell, pointer));
        }

        public static IEnumerator CreateSnap(IEnumerable<FaceData> faces, TechType room, Transform root, Base seabase, BaseCell cell, SnapHolder pointer)
        {
            foreach (FaceData faceData in faces)
            {
                faceData.seabaseFaces ??= new Base.Face[0];

                BaseFaceIdentifier.CreateSnap(faceData, room, root, out BaseFaceIdentifier identifier);
                identifier.Link(seabase, cell, faceData.seabaseFaces, pointer);

                yield return null;
            }

            pointer.UpdateFaces(seabase);
        }
    }
}
