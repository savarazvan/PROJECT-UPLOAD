using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthSlider : MonoBehaviour
{
    Text healthText;
    Slider healthSlider;
    void Start()
    {
        healthText = GetComponentInChildren<Text>();
        healthSlider = GetComponentInChildren<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        healthSlider.maxValue = PlayerHealth.maxHealth;
        healthSlider.value = PlayerHealth.currentHealth;
        healthText.text = PlayerHealth.currentHealth.ToString() + "/" + PlayerHealth.maxHealth.ToString();
    }
}
