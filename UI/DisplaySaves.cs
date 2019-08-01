using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class DisplaySaves : MonoBehaviour
{
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
        int i = 1;

        BinaryFormatter formatter = new BinaryFormatter();
        
        foreach(RectTransform saveSlot in transform)
        {
            if (saveSlot.tag == "SaveSlot")
            {
                Text slotText = saveSlot.GetComponentInChildren<Text>();
                if (File.Exists(Application.persistentDataPath + "/saveslot" + i + ".data"))
                {
                    FileStream stream = new FileStream(Application.persistentDataPath +
                        "/saveslot" + i + ".data", FileMode.Open);

                    if (stream.Length == 0)
                    {
                        slotText.text = "Empty slot";
                        stream.Close();
                        i++;
                        return;
                    }

                    PlayerData playerData = formatter.Deserialize(stream) as PlayerData;
                    slotText.text = playerData.date + System.Environment.NewLine + playerData.time;

                    stream.Close();
                }

                else slotText.text = "Empty slot";
                i++;
            }
        }
       
        lastUpdated = Time.time;
    }
}
