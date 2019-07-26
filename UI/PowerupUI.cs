using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerupUI : MonoBehaviour
{
    Image powerupIcon;
    Slider powerupSlider;
    PowerupSlot powerup;

    void Start()
    {
        powerupIcon = GetComponentInChildren<Image>();
        powerupSlider = GetComponentInChildren<Slider>();
        powerup = GameObject.FindGameObjectWithTag("Player").GetComponent<PowerupSlot>();
    }

    void Update()
    {
        if(powerup.powerupSlot==null) 
        {
            powerupSlider.gameObject.SetActive(false);
            powerupIcon.gameObject.SetActive(false);
            return;
        }

        //-----------------------------------------------------

        if(!powerupSlider.gameObject.activeSelf)
        {
            powerupSlider.gameObject.SetActive(true);
            powerupIcon.gameObject.SetActive(true);
        }

        //----------------------------------------------------

        if (powerup.powerupActive)
            powerupSlider.maxValue = powerup.powerupSlot.duration;
        else if (powerup.powerupInCooldown)
            powerupSlider.maxValue = powerup.powerupCooldown;
      
        powerupIcon.sprite = powerup.powerupSlot.sprite;
        powerupSlider.value = powerup.powerupTimer;
        
    }
}
