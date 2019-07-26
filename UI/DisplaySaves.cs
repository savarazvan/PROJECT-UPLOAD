using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class DisplaySaves : MonoBehaviour
{
    public Text slot1, slot2, slot3;
    public float updateFreq, lastUpdated;

    private void Start()
    {
        updateSlots();
    }

    private void Update()
    {
        if(Time.time>lastUpdated+updateFreq)
        {
            updateSlots();
        }
    }

    void updateSlots()
    {
        BinaryFormatter formatter = new BinaryFormatter();

        if (File.Exists(Application.persistentDataPath + "/saveslot1.data"))
        {
            FileStream stream = new FileStream(Application.persistentDataPath + "/saveslot1.data", FileMode.Open);
            PlayerData playerData = formatter.Deserialize(stream) as PlayerData;
            slot1.text = playerData.date + System.Environment.NewLine + playerData.time;
            stream.Close();
        }
        else slot1.text = "Empty slot";

        if (File.Exists(Application.persistentDataPath + "/saveslot2.data"))
        {
            FileStream stream = new FileStream(Application.persistentDataPath + "/saveslot2.data", FileMode.Open);
            PlayerData playerData = formatter.Deserialize(stream) as PlayerData;
            slot1.text = playerData.date + System.Environment.NewLine + playerData.time;
            stream.Close();
        }
        else slot2.text = "Empty slot";

        if (File.Exists(Application.persistentDataPath + "/saveslot3.data"))
        {
            FileStream stream = new FileStream(Application.persistentDataPath + "/saveslot3.data", FileMode.Open);
            PlayerData playerData = formatter.Deserialize(stream) as PlayerData;
            slot1.text = playerData.date + System.Environment.NewLine + playerData.time;
            stream.Close();
        }
        else slot3.text = "Empty slot";

        lastUpdated = Time.time;
    }
}
