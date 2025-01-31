using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Project.Scripts.Core.Debuger;
using UnityEngine;

namespace Seshihiko.Systems.Save
{
    public static class Serializator
    {
        private static string CreatePath(string path)
        {
            return Application.streamingAssetsPath + path;
        }

        public static void SaveData<T>(T data, string path)
        {
            using (var file = File.Create(CreatePath(path)))
            {
                var binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(file, data);
            }
        }

        public static bool LoadData<T>(out T data, string path) where T : new()
        {
            var pathEdited = CreatePath(path);

            if (File.Exists(pathEdited))
            {
                var binaryFormatter = new BinaryFormatter();
                
                using (var file = File.Open(pathEdited, FileMode.Open))
                {
                    data = (T)binaryFormatter.Deserialize(file);
                    DebugConsole.Log($"Loaded in path: {pathEdited}");
                    return true;
                }
            }
            
            data = new T();
            return false;
        }
    }
}