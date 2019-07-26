using UnityEngine;

public class RoomList : MonoBehaviour
{
    public IsRoom[] rooms()
    {
        IsRoom[] isRooms;
        isRooms = GetComponentsInChildren<IsRoom>(true);
        return isRooms;
    }
    
}
