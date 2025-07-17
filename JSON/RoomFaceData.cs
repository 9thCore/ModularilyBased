using Nautilus.Json;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace ModularilyBased.JSON
{
    /// <summary>
    /// Main way of interfacing with the library's face definitions.
    /// </summary>
    public class RoomFaceData : JsonFile
    {
        [JsonIgnore]
        private readonly string jsonFilePath;
        /// <summary>
        /// Standard property that json files must have.
        /// </summary>
        public override string JsonFilePath => jsonFilePath;

        // Index by Base.CellType. Probably will require another identifier for custom base pieces, hence string instead of enum.
        [JsonProperty]
        internal Dictionary<string, List<FaceData>> storage = new();

        internal RoomFaceData(string jsonFilePath)
        {
            this.jsonFilePath = jsonFilePath;
        }

        /**
         * <summary>
         * Add the directory /Subnautica/BepInEx/plugins/<paramref name="assembly"/>/<paramref name="directory"/> as a possible <c>Definitions</c> directory.
         * </summary>
         * <remarks>
         * C#-style <c>partial</c> support is implemented - in other words, two files with the same name in different <c>Definitions</c> directories will be merged. <b>Run before loading in a save, preferably on plugin load, else the definitions might not be read.</b>
         * </remarks>
         * <returns>
         * <c>false</c> if the directory does not exist, <c>true</c> otherwise.
         * </returns>
         */
        public static bool TryAddRoomDefinitionsDirectory(string directory, Assembly assembly = null)
        {
            assembly ??= Assembly.GetCallingAssembly();
            string assemblyPath = Path.GetDirectoryName(assembly.Location);
            string fullPath = Path.Combine(assemblyPath, directory);
            if (!Directory.Exists(fullPath))
            {
                return false;
            }

            DefinitionsPaths.Add(fullPath);
            return true;
        }

        /**
         * <summary>
         * Add <paramref name="faces"/> to the list of faces under <paramref name="identifier"/>/<paramref name="extraID"/>.
         * </summary>
         * <remarks>
         * Fails if the room's <c>Definition</c> does not exist under any of the given directories (<see cref="TryAddRoomDefinitionsDirectory(string, Assembly)"/>). <b>Run before loading in a save, preferably on plugin load, else the <paramref name="faces"/> might not be read.</b>
         * </remarks>
         * <returns>
         * <c>false</c> if the method fails, <c>true</c> otherwise.
         * </returns>
         */
        public static bool TryAddFaces(string identifier, string extraID, IEnumerable<FaceData> faces)
        {
            if (!TryGetFaces(identifier, extraID, out List<FaceData> storage))
            {
                return false;
            }

            storage.AddRange(faces);
            return true;
        }

        /**
         * <summary>
         * Add <paramref name="face"/> to the list of faces under <paramref name="identifier"/>/<paramref name="extraID"/>.
         * </summary>
         * <remarks>
         * Fails if the room's <c>Definition</c> does not exist under any of the given directories (<see cref="TryAddRoomDefinitionsDirectory(string, Assembly)"/>). <b>Run before loading in a save, preferably on plugin load, else the <paramref name="face"/> might not be read.</b>
         * </remarks>
         * <returns>
         * <c>false</c> if the method fails, <c>true</c> otherwise.
         * </returns>
         */
        public static bool TryAddFace(string identifier, string extraID, FaceData face)
        {
            if (!TryGetFaces(identifier, extraID, out List<FaceData> storage))
            {
                return false;
            }

            storage.Add(face);
            return true;
        }

        /**
         * <summary>
         * Tries fetching the <paramref name="faces"/> for the given room, identified by <paramref name="identifier"/><paramref name="extraID"/>. Will use the cache on subsequent calls.
         * </summary>
         * <remarks>
         * Fails if there are no available faces.
         * </remarks>
         * <returns>
         * <c>false</c> if the method fails, <c>true</c> otherwise.
         * </returns>
         */
        public static bool TryGetFaces(string identifier, string extraID, out List<FaceData> faces)
        {
            if (faceDataCache.TryGetValue(identifier, out Dictionary<string, List<FaceData>> storage))
            {
                if (storage.TryGetValue(extraID, out faces))
                {
                    return true;
                }
            } else
            {
                storage = new();
                faceDataCache.Add(identifier, storage);
            }

            faces = new();
            storage.Add(extraID, faces);

            foreach (string definitionsDirectory in DefinitionsPaths)
            {
                string json = GetJsonPath(definitionsDirectory, identifier);
                if (!File.Exists(json))
                {
                    continue;
                }

                RoomFaceData data = new RoomFaceData(json);
                data.LoadWithConverters(false, new Int3Converter());

                if (data.storage.TryGetValue(extraID, out List<FaceData> partialFaces))
                {
                    faces.AddRange(partialFaces);
                }
            }

            return faces.Count > 0;
        }

        internal static bool TryLoadRoomData(string identifier, out RoomFaceData data)
        {
            if (roomCache.TryGetValue(identifier, out data))
            {
                return true;
            }

            foreach (string definitionsDirectory in DefinitionsPaths)
            {
                string json = GetJsonPath(definitionsDirectory, identifier);
                if (!File.Exists(json))
                {
                    continue;
                }

                data = new RoomFaceData(json);
                data.LoadWithConverters(false, new Int3Converter());
                roomCache.Add(identifier, data);
                return true;
            }

            data = null;
            return false;
        }

        internal static string GetJsonPath(string definitionsDirectory, string filename)
        {
            return Path.Combine(definitionsDirectory, $"{filename}.json");
        }

        private static List<string> definitionsPaths;
        internal static List<string> DefinitionsPaths => definitionsPaths ??= new List<string>();

        internal static readonly Dictionary<string, RoomFaceData> roomCache = new();
        internal static readonly Dictionary<string, Dictionary<string, List<FaceData>>> faceDataCache = new();
    }
}
