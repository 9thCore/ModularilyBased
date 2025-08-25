using ModularilyBased.API.Register;
using System.Collections.Generic;

namespace ModularilyBased.Functionality
{
    internal static class RoomFaceHolder
    {
        public static readonly Dictionary<TechType, CellTypeMapping> RoomData = new();

        public static void AddFace(TechType techType, Base.CellType cellType, FaceData face)
        {
            if (!RoomData.TryGetValue(techType, out CellTypeMapping mapping))
            {
                mapping = new CellTypeMapping();
                RoomData[techType] = mapping;
            }

            if (!mapping.TryGetValue(cellType, out List<FaceData> existingFaces))
            {
                existingFaces = new List<FaceData>();
                mapping[cellType] = existingFaces;
            }

            existingFaces.Add(face);
        }

        public static List<FaceData> GetMatchingFaces(TechType techType, Base.CellType cellType)
        {
            List<FaceData> result = new();

            if (RoomData.TryGetValue(techType, out CellTypeMapping mapping))
            {
                if (mapping.TryGetValue(cellType, out List<FaceData> exactMatch))
                {
                    result.AddRange(exactMatch);
                }

                if (mapping.TryGetValue(Base.CellType.Empty, out List<FaceData> genericMatch))
                {
                    result.AddRange(genericMatch);
                }
            }

            return result;
        }

        internal class CellTypeMapping : Dictionary<Base.CellType, List<FaceData>> { }
    }
}
