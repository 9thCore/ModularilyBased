using HarmonyLib;
using ModularilyBased.JSON;
using ModularilyBased.Library;
using System;
using UnityEngine;

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
            pointer.SetSibling(cell.transform);
            GameObject sibling = pointer.root;

            if (!RoomFaceData.TryLoadRoomData(filename, out RoomFaceData roomData)
                || !roomData.storage.ContainsKey(extraID))
            {
                return;
            }

            foreach (FaceData faceData in roomData.storage[extraID])
            {
                BaseFaceIdentifier.CreateSnap(faceData, room, sibling.transform, out BaseFaceIdentifier identifier);
                identifier.Link(seabase, cell, faceData.seabaseFace);
            }
        }
    }
}
