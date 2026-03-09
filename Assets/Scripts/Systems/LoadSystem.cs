using System.IO;
using UnityEngine;

public static class LoadSystem
{
    public static SaveData LoadGameState()
    {
        try
        {
            string filePath = Application.persistentDataPath + SaveSystem.FILENAME_SAVEDATA;
            string fileContent = File.ReadAllText(filePath);
            SaveData saveData = JsonUtility.FromJson<SaveData>(fileContent);
            return saveData;
        }
        catch
        {
            Debug.Log("No save data found, starting new game.");
            return null;
        }
    }
}
