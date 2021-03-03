using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class Save_Data
{
    public static void SavePlayerData()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.rescue";
        FileStream stream = new FileStream(path, FileMode.Create);

        Player_Data data = new Player_Data();

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static Player_Data LoadPlayerData()
    {
        string path = Application.persistentDataPath + "/player.rescue";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            Player_Data data = (Player_Data)formatter.Deserialize(stream);

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
