using System.IO;
using UnityEngine;

namespace Orchard.Game
{
    public class LoadLevel
    {
        public JsonSavedLevelData GetLevelData(int numberLevel)
        {
#if UNITY_ANDROID
            string filePath = $"Levels/{numberLevel.ToString("D4")}";

            TextAsset txtLevel = Resources.Load<TextAsset>(filePath) as TextAsset;

            return JsonUtility.FromJson<JsonSavedLevelData>(AesEncryption.Decrypt(txtLevel.text));
#else
            string folderName = Application.streamingAssetsPath + $"/Levels/";
            string file = numberLevel.ToString("D4");

            if (File.Exists(folderName + file + ".json"))
            {
                StreamReader sr = File.OpenText(folderName + file + ".json");
                string jsonLD = sr.ReadToEnd();
                sr.Close();

                return JsonUtility.FromJson<JsonSavedLevelData>(AesEncryption.Decrypt(jsonLD));
            }
            else
            {
                //Debug.LogError("Could not Open the file: " + file + " for reading.");

                if (numberLevel == 1)
                    return null;

                return GetLevelData(1);
            }
#endif
        }
    }
}
