using UnityEngine;

[System.Serializable]
public class PlayerData 
{
    public int maxHealth, currentHealh;
    public float[] position = new float[3];
    public string currentRoom;
    public int powerupID;
    public string date;
    public string time;

    public PlayerData(Transform transform, Powerup powerup)
    {
        date = System.DateTime.Now.ToString("dd/MM/yyyy");
        time = System.DateTime.Now.ToString("HH:mm");
        maxHealth = PlayerHealth.maxHealth;
        currentHealh = PlayerHealth.currentHealth;
        position[0] = transform.position.x;
        position[1] = transform.position.y;
        position[2] = transform.position.z;
        currentRoom = GameMaster.currentRoom.name;
        powerupID = powerup.id;
    }

   
}
