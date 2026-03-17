using Newtonsoft.Json;
using System.IO;
using UnityEngine;

public static class SavingSystem
{
    static string path = Application.persistentDataPath + "/M6-BW2-Save.json";

    private static bool ExistAPath()
    {
        return File.Exists(path);
    }

    public static bool Save()
    {
        SaveData data = GameState.Instance.GetSaveData();

        string jsonText = JsonConvert.SerializeObject(data, Formatting.Indented);

        File.WriteAllText(path, jsonText);
        return true;
    }

    public static SaveData Load()
    {
        if (!ExistAPath())
        {
            Debug.LogError($"Unable to load data in {path}");
            return null;
        }

        string jsonText = File.ReadAllText(path);

        SaveData data = JsonConvert.DeserializeObject<SaveData>(jsonText);

        return data;
    }
}
