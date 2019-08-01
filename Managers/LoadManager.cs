using UnityEngine;

public class LoadManager
{
   public static int currentSlot = 0;

   public static void Load(int slot)
    {
        IsRoom[] rooms;
        PlayerData data = SaveSystem.LoadPlayer(slot);
        Transform transform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        PlayerHealth.maxHealth = data.maxHealth;
        PlayerHealth.currentHealth = PlayerHealth.maxHealth;
        rooms = GameObject.Find("Campaign").GetComponent<RoomList>().rooms();
        
        //--------------------------------------------------------------------

        for (int i=0; i<rooms.Length; i++)
        {
            IsRoom thisRoom = rooms[i];

            if (thisRoom.name != data.currentRoom)
                thisRoom.gameObject.SetActive(false);
            else
            {
                thisRoom.gameObject.SetActive(true);
                transform.position = new Vector3(data.position[0], data.position[1], data.position[2]);
                GameMaster.currentRoom = thisRoom.gameObject;
            }
        }

        //--------------------------------------------------------------------

        loadPowerup(data.powerupID);

        currentSlot = slot;
        
    }

    public static void loadPowerup(int ID)
    {
        PowerupSlot powerup = GameObject.FindGameObjectWithTag("Player").GetComponent<PowerupSlot>();
        switch (ID)
        {
            case 0:
                {
                    powerup.powerupSlot = null;
                    break;
                }
            case 1:
                {
                    powerup.powerupSlot = powerup.speed;
                    break;
                }
            case 2:
                {
                    powerup.powerupSlot = powerup.regeneration;
                    break;
                }
            case 3:
                {
                    powerup.powerupSlot = powerup.timeshift;
                    break;
                }
            case 4:
                {
                    powerup.powerupSlot = powerup.treathTrack;
                    break;
                }
            default:
                {
                    Debug.LogError(ID + " is not a valid ID");
                    break;
                }
        }
    }
}
