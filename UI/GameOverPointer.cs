using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPointer : MonoBehaviour
{
    Vector2[] positions = { new Vector2(-130, -100), new Vector2(120, -100) };
    public int pointer = 0;
    RectTransform rectTransform;
    GameMaster gameMaster;
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        gameMaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            pointer++;
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            pointer--;
        updatePos();
        
    }
    void updatePos()
    {
       
        if (pointer < 0)
            pointer = positions.Length - 1;
        else if (pointer >= positions.Length)
            pointer = 0;
        rectTransform.localPosition = Vector3.Lerp(rectTransform.localPosition, positions[pointer], 12f * Time.deltaTime);

    }

    
   
}
