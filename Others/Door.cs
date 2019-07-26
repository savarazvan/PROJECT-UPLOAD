using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject leadsToRoom;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void roomSwitcher()
    {
        GameMaster.roomSwitch(leadsToRoom);//GameMasterul se va ocupa cu schimbatul camerei in care playerul se afla cu leadsToRoom
    }
}
