using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class Save
{

    public static void SaveGame(CombatSheninagens player)
    {
        BinaryFormatter bf = new BinaryFormatter();
        string path = Application.persistentDataPath + "/Saves/SaveData" + PlayerPrefs.GetInt("SaveFileNum") + ".merlinscastleofarksave";
        // string path = ("C:/Users/bkwoo/SaveData.etphonehome");
        FileStream stream = new FileStream(path, FileMode.Create);
        PlayerData data = new PlayerData(player);
        Debug.Log(data.orangeItems[1]);
        bf.Serialize(stream, data);
        stream.Close();
        Debug.Log("Game data saved!");
    }

    public static void SaveGameTwo(LevelSelector player)
    {
        BinaryFormatter bf = new BinaryFormatter();
        string path = Application.persistentDataPath + "/Saves/SaveData" + PlayerPrefs.GetInt("SaveFileNum") + ".merlinscastleofarksave";
        // string path = ("C:/Users/bkwoo/SaveData.etphonehome");
        FileStream stream = new FileStream(path, FileMode.Create);
        PlayerData data = new PlayerData(player);
        bf.Serialize(stream, data);
        stream.Close();
        Debug.Log("Game data saved!");
    }

    public static void SaveGameThree(PlayButton player)
    {
        BinaryFormatter bf = new BinaryFormatter();
        string path = Application.persistentDataPath + "/Saves/SaveData" + PlayerPrefs.GetInt("SaveFileNum") + ".merlinscastleofarksave";
        // string path = ("C:/Users/bkwoo/SaveData.etphonehome");
        FileStream stream = new FileStream(path, FileMode.Create);
        PlayerData data = new PlayerData(player);
        bf.Serialize(stream, data);
        stream.Close();
        Debug.Log("Game data saved!");
    }

    public static PlayerData LoadPlayer ()
    {
        string path = Application.persistentDataPath + "/Saves/SaveData" + PlayerPrefs.GetInt("SaveFileNum") + ".merlinscastleofarksave";
        if (File.Exists(path))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            PlayerData data = bf.Deserialize(stream) as PlayerData;
            stream.Close();
            Debug.Log("Retrieved save data!");
            return data;
        }
        else
        {
            Debug.LogWarning("Unable to find save data in " + path + ". Sorry!");
            return null;
        }
    }
}
