using UnityEngine;
using UnityEngine.InputSystem;

public class ArmRotation : MonoBehaviour
{
    private Quaternion armRotation;
    public GameObject player;
    public GameObject melee;
    public int dir;
    private float H, V;
    public bool facingRight;
    int previousDir;
    public InputAction aiming;

    private void Awake()
    {
        aiming.performed += ctx => setValues(ctx.ReadValue<Vector2>());
    }

    //---------------------------------------------

    void Start()
    {
        facingRight = true;
    }

    void setValues(Vector2 vector)
    {
        V = vector.y;
        H = vector.x;
    }

    //---------------------------------------------

    void Update()
    {
        facingRight = player.GetComponent<CharacterMovement>().facingRight;
       
        if (H > 0 && facingRight)
        {
            if (V > 0)
                dir = 2; // sus dreapta
            else if (V < 0)
                dir = 4; // jos dreapta

            if (V == 0)
                dir = 3; // dreapta
        }
        else if (H < 0 && !facingRight)
        {
            if (V > 0)
                dir = 8; // stanga sus
            else if (V < 0)
                dir = 6; // stanga jos
            if (V == 0)
                dir = 7;
        }
        if (V > 0)
        {
            if (H > 0 && facingRight)
                dir = 2; // sus dreapta
            else if (H < 0 && !facingRight)
                dir = 8; // stanga jos
            if (H == 0)
                dir = 1; // sus
        }
        if (V < 0)
        {
            if (H > 0 && facingRight)
                dir = 4; // sus dreapta
            else if (H < 0 && !facingRight)
                dir = 6; // stanga jos
            if (H == 0)
                dir = 5; // jos
        }


        if (V == 0)
            if (H < 0 && !facingRight)
                dir = 7; //stanga
            else if (H==0)
                dir = 3; // dreapta sau stanga, depinde din ce parte te uiti

        if (previousDir != dir)
        {
            CheckDir();
        }

       
    }

    //---------------------------------------------

    private void CheckDir()
    { 
        if (dir == 1)
        {
            transform.rotation = Quaternion.identity;
            transform.Rotate(0, 0, 90, Space.Self);
            
        }
        if (dir == 2)
        {
             transform.rotation = Quaternion.identity;
             transform.Rotate(0, 0, 45, Space.Self);
           
        }
        if (dir == 3)
        {
            transform.rotation = Quaternion.identity;       
        }
        if (dir == 4)
        {
            transform.rotation = Quaternion.identity;
            transform.Rotate(0, 0, -45, Space.Self);
        }
        if (dir == 5)
        {
            transform.rotation = Quaternion.identity;
            transform.Rotate(0, 0, -90, Space.Self);
        }
        if (dir == 6)
        {
            transform.rotation = Quaternion.identity;
            transform.Rotate(0, 0, -45, Space.Self);
        }
        if (dir == 8)
        {
            transform.rotation = Quaternion.identity;
            transform.Rotate(0, 0, 45, Space.Self);
        }

        previousDir = dir;

    }

    //---------------------------------------------

    private void OnEnable()
    {
        aiming.Enable();
    }

    private void OnDisable()
    {
        aiming.Disable();
    }
}

