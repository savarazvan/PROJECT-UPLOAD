using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PistolChargeShot : MonoBehaviour
{
    Slider slider;
    float posx;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.value = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        slider.value += (float)0.3 * Time.deltaTime * 4;
        if (Input.GetKeyUp(KeyCode.Space))
            slider.value = 0;
    }
    
}
