using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    public static float moveSpeed = 30f;
    public float jumpForce;
    private float velocity;
    public int jumpCount;
    public static bool candoublejump, canDash;
    public LayerMask whatIsGround;
    public bool facingRight, canClimb;
    public GameMaster gameMaster;
    Animator anim;
    Rigidbody2D rb;
    [HideInInspector]public Vector2 groundDir;
    public Animator animator;
    public PlayerInput controls;
    Keyboard keyboard;
    [SerializeField]private InputAction movement, jumping, climbing;
   
    void Awake()
    {
        controls = new PlayerInput();

        movement.performed += ctx =>
        {
            velocity = ctx.ReadValue<float>();
            checkFlip();
        };

        movement.canceled += ctx =>
        {
            velocity = ctx.ReadValue<float>();
            checkFlip();
        };

        jumping.performed += ctx => jump();
        climbing.performed += ctx => climbLadder(ctx.ReadValue<float>());

        controls.Player.Pause.performed += ctx => gameMaster.pauseGame(this, false);

        keyboard = Keyboard.current;
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        groundDir = Vector2.down;
        gameMaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }

    void OnTriggerEnter2D(Collider2D trigger) 
    {


        if (trigger.gameObject.tag == "Pit")
            PlayerHealth.currentHealth = 0;
        //Scara
        else if (trigger.gameObject.tag == "Ladder")
            canClimb = true;


        else if (trigger.gameObject.tag == "Exit/Horizontal")
        {
            trigger.GetComponent<Door>().roomSwitcher();
            transform.position = new Vector2(transform.position.x + 10, transform.position.y);
        }

        else if (trigger.gameObject.tag == "Exit/Vertical")
        {
            trigger.GetComponent<Door>().roomSwitcher();
            transform.position = new Vector2(transform.position.x, transform.position.y + 5);
        }

        else if (trigger.gameObject.tag == "Exit/Vertical/Down")
        {
            trigger.GetComponent<Door>().roomSwitcher();
            transform.position = new Vector2(transform.position.x, transform.position.y - 5);
        }
    }
  

    private void OnTriggerExit2D(Collider2D trigger)
    {
      
        if (trigger.gameObject.tag == "Ladder")
            canClimb = false;

    }

    //---------------------------------------------------------------------------------

    void checkFlip()
    {

        if (velocity > 0 && !facingRight)
        {

            Flip();
            facingRight = true;
        }

        else if (velocity < 0 && facingRight)
        {

            Flip();
            facingRight = false;
        }       
    }

    //---------------------------------------------------------------------------------

    void jump()
    {
        if (grounded())
        {
            GameObject.FindObjectOfType<AudioManager>().play("Jump");

            rb.velocity = (new Vector2(rb.velocity.x, jumpForce));
            candoublejump = true;           
            if (UpgradeManager.jumpTimes < 20)
                UpgradeManager.jumpTimes++;
            UpgradeManager.checkUnlocks();
        }

        else if (!grounded())
        {
            if (candoublejump && UpgradeManager.doubleJump)
            {
                GameObject.FindObjectOfType<AudioManager>().play("Jump");

                rb.velocity = (new Vector2(rb.velocity.x, jumpForce));
                candoublejump = false;
            }
        }
    }

    //---------------------------------------------------------------------------------

    void climbLadder(float dir)
    {
        if (!canClimb)
            return;
        rb.velocity = new Vector2(0, 10 * dir);
    }

    //---------------------------------------------------------------------------------

    public void Update()
    {

        rb.velocity = new Vector2(moveSpeed * velocity * Time.deltaTime * 60, rb.velocity.y);

        if (keyboard.f1Key.wasPressedThisFrame)
        {
            SaveSystem.SavePlayer(GetComponent<Transform>(), 1, GetComponent<PowerupSlot>().powerupSlot);
        }

       if(keyboard.f2Key.wasPressedThisFrame)
        {
            LoadManager.Load(1);
        }

        animator.SetFloat("speed", Mathf.Abs(rb.velocity.x));

       /* (canClimb)
            rb.velocity = new Vector2(0, 0);
            */
    }

    //---------------------------------------------------------------------------------

    public bool grounded()
    {
        Vector2 position = transform.position;

        float distance = 2f;
        Debug.DrawRay(position, groundDir, Color.green);

        RaycastHit2D hit = Physics2D.Raycast(position, groundDir, distance, whatIsGround);
        if (hit.collider != null)
        {
            return true;
        }
        return false;
    }

    void Flip()
        {
            Vector3 tempScale = transform.localScale;
            tempScale.x *= -1;
            transform.localScale = tempScale;
        }


    //---------------------------------------------------------------------------------


    public void OnEnable()
    {
        controls.Enable();
        movement.Enable();
        jumping.Enable();
        climbing.Enable();
        GetComponentInChildren<PlayerInventory>().showWheel.Enable();
        GetComponentInChildren<PlayerInventory>().attackKey.Enable();
    }

    public void OnDisable()
    {
        controls.Disable();
        movement.Disable();
        jumping.Disable();
        climbing.Disable();
        GetComponentInChildren<PlayerInventory>().showWheel.Disable();
        GetComponentInChildren<PlayerInventory>().attackKey.Disable();
    }

}


