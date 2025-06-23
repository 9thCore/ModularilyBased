using Nautilus.Extensions;
using Nautilus.Json;
using Nautilus.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace ModularilyBased.JSON
{
    public class RoomFaceData : JsonFile
    {
        [JsonIgnore]
        public readonly string filename;

        [JsonIgnore]
        private string jsonFilePath;
        public override string JsonFilePath => jsonFilePath ??= Path.Combine(DefinitionsPath, $"{filename}.json");

        // Index by Base.CellType. Probably will require another identifier for custom base pieces, hence string instead of enum.
        public Dictionary<string, List<FaceData>> storage = new();

        public RoomFaceData(string filename)
        {
            this.filename = filename;
        }

        public static bool TryLoadRoomData(string identifier, out RoomFaceData data)
        {
            if (faceCache.TryGetValue(identifier, out data))
            {
                return true;
            }

            data = new RoomFaceData(identifier);

            if (!Directory.Exists(Path.GetDirectoryName(data.JsonFilePath))
                || !File.Exists(data.JsonFilePath))
            {
                data = null;
                return false;
            }

            data.LoadWithConverters(false, new Int3Converter());
            return true;
        }

        private static string assemblyPath;
        public static string AssemblyPath => assemblyPath ??= Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        private static string definitionsPath;
        public static string DefinitionsPath => definitionsPath ??= Path.Combine(AssemblyPath, "Definitions");

        public static readonly Dictionary<string, RoomFaceData> faceCache = new();
    }
}
