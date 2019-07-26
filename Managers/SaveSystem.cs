using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    //--------------------------------------------------------------------

    public static void SavePlayer(Transform transform, int slot, Powerup powerup)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/saveslot" + slot + ".data";
        FileStream stream = new FileStream(path, FileMode.Create);
        Debug.Log("saved on path " + path);
        PlayerData playerData = new PlayerData(transform, powerup);
        formatter.Serialize(stream, playerData);
        stream.Close();
    }

    //--------------------------------------------------------------------

    public static PlayerData LoadPlayer(int slot)
    {
        string path = Application.persistentDataPath + "/saveslot" + slot + ".data";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData playerData = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            return playerData;

        }
     //--------------------------------------------------------------------
        else
        {
            Debug.LogError("No save file found in " + path);
            return null;
        }
    }
}
