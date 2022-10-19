using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveSerial : MonoBehaviour
{
    public static Dictionary<string, int> playerStats = new Dictionary<string, int>();
    public static void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath
          + "/MySaveData.dat");
        SaveData data = new SaveData();
        data.stats = playerStats;
        bf.Serialize(file, data);
        file.Close();
        Debug.Log("Game data saved!");
    }

    public static void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath
          + "/MySaveData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file =
              File.Open(Application.persistentDataPath
              + "/MySaveData.dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();
            playerStats = data.stats;
            Debug.Log("Game data loaded!");
        }
        else
            Debug.LogError("There is no save data!");
    }
    public static void ResetData()
    {
        if (File.Exists(Application.persistentDataPath
          + "/MySaveData.dat"))
        {
            File.Delete(Application.persistentDataPath
              + "/MySaveData.dat");
            playerStats.Clear();
            Debug.Log("Data reset complete!");
        }
        else
            Debug.LogError("No save data to delete.");
    }
}

 [Serializable]
class SaveData
{
    public Dictionary<string, int> stats = new Dictionary<string, int>();

}