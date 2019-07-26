using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsUI : MonoBehaviour
{
    public static Text button_E, button_Q, button_W, button_Space;
    PowerupSlot powerupSlot;

    CharacterMovement characterMovement;
    void Start()
    {
        InitializeUiButtons();
        powerupSlot = GameObject.FindGameObjectWithTag("Player").GetComponent<PowerupSlot>();
    }

    // Update is called once per frame
    void Update()
    {
        if(characterMovement==null)
            characterMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMovement>();
        if(powerupSlot==null)
            powerupSlot = GameObject.FindGameObjectWithTag("Player").GetComponent<PowerupSlot>();

        //---------------------------------------------------------------

        if (characterMovement.grounded())
        {
            button_W.text = "Jump";
        }

        else button_W.text = " ";

        //---------------------------------------------------------------
        if (powerupSlot.powerupSlot == null)
        {
            button_Q.text = " ";
        }
        else
        {
            if (powerupSlot.powerupActive || powerupSlot.powerupInCooldown)
                button_Q.text = " ";
            else button_Q.text = powerupSlot.powerupSlot.name;
        }
        //---------------------------------------------------------------
    }


    private void InitializeUiButtons()
    {
        button_E = gameObject.transform.Find("Buttons_E").gameObject.GetComponent<Text>();
        button_Q = gameObject.transform.Find("Buttons_Q").gameObject.GetComponent<Text>();
        button_W = gameObject.transform.Find("Buttons_W").gameObject.GetComponent<Text>();
        button_Space = gameObject.transform.Find("Buttons_Spacebar").gameObject.GetComponent<Text>();
    }
}
