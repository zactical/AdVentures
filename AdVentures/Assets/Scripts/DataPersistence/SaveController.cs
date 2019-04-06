using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SaveController
{
    public static void Load(HighScoreData data, string filepath = "saveNameTest")
    {
        var path = Path.Combine(Application.persistentDataPath, string.Format("{0}.json", filepath));

        if (File.Exists(path))
            JsonUtility.FromJsonOverwrite(File.ReadAllText(path), data);            
    }

    public static void Save(HighScoreData data)
    {
        data.PrepareForSave();
        var output = JsonUtility.ToJson(data);

        File.WriteAllText(Path.Combine(Application.persistentDataPath, string.Format("{0}.json", "saveNameTest")), output);
    }

    public static void DeleteSaveFile(string filepath = "saveNameTest")
    {
        File.Delete(Path.Combine(Application.persistentDataPath, string.Format("{0}.json", "saveNameTest")));
    }
}
