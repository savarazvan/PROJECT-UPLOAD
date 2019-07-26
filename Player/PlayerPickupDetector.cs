using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class PlayerPickupDetector : MonoBehaviour
{
    PowerupSlot powerup;
    PlayerInput controls;
    Powerup pup;
    GameObject obj;

    private void Awake()
    {
        controls = new PlayerInput();
        controls.Player.Interact.performed += ctx => pickup();
    }

    void Start()
    {
        powerup = GetComponent<PowerupSlot>();
        ButtonsUI.button_E.text = "       ";

    }

    void pickup()
    { 
        if (pup == null)
            return;
        powerup.powerupSlot = pup;
        Destroy(obj);
        ButtonsUI.button_E.text = " ";
        Debug.Log("picked up" + pup.name);
    }
   
    private void OnTriggerEnter2D(Collider2D pickup)
    {
        
        if (pickup.gameObject.tag == "Pickup/Speed")
            setPickup(powerup.speed, pickup.gameObject);

        else if (pickup.gameObject.tag == "Pickup/Regeneration")
            setPickup(powerup.regeneration, pickup.gameObject);

        else if (pickup.gameObject.tag == "Pickup/TimeShift")
            setPickup(powerup.timeshift, pickup.gameObject);

        else if (pickup.gameObject.tag == "Pickup/TreathTrack")
            setPickup(powerup.treathTrack, pickup.gameObject);
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        ButtonsUI.button_E.text = " ";
        obj = null;
        pup = null;
    }

    void setPickup(Powerup powerup, GameObject _object)
    {
        ButtonsUI.button_E.text = "Pick up";
        pup = powerup;
        obj = _object;
        Debug.Log(powerup.name + " " + obj.name);
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

}
