using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    public float dashForce;
    Rigidbody2D rb;
    [SerializeField]private float Atimer, Dtimer;
    private bool canDash;
    private int pressedD, pressedA;
    public float pressDelay=1f;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
            if(Input.GetKeyDown(KeyCode.D))
        {
            
            
            if (pressedD == 1) Dtimer = Time.time;

            else if (pressedD > 1 && Time.time - Dtimer < pressDelay)
            {
                Debug.Log("am facut dash");
                pressedD = 0;
                Dtimer = 0;
                
                

            }
            else if (pressedD > 2 || Time.time - Dtimer > 1) pressedD = 0;

        }
            if(Input.GetKeyDown(KeyCode.A))
        {
            if (Dtimer > 0f)
                Dtimer = 0f;
            pressedA++;
            if (pressedA == 1) Atimer = Time.time;

            else if (pressedA > 1 && Time.time - Atimer < pressDelay)
            {
                Debug.Log("am facut dash");
                pressedA = 0;
                Atimer = 0;
                rb.AddForce(new Vector2(-dashForce, 0), ForceMode2D.Impulse);


            }
            else if (pressedA > 2 || Time.time - Atimer > 1) pressedA = 0;

        }


    }



}
