using UnityEngine;
using System.Collections;

[RequireComponent(typeof (Rigidbody2D))]
public class EnemyAI : MonoBehaviour {

    public float updateRate = 2f;
    public Transform player;
    public float enemySpeed=20, meleeRange=3, meleeDelay=1.5f;  
    private float lastAttackTime;
    public int baseDamage=8;
    Rigidbody2D rb;
    Transform playerDirection;
    public Animator animator;
    [HideInInspector] public Vector3 playerPos;
    bool coroutineStarted;
    RaycastHit2D hit;
    public LayerMask whatIsGround;
    public float jumpDistance;
    float checkPitDist = 2.5f;
    public bool facingRight;
    EnemyWeapon enemyWeapon;
    float weaponRange;
    bool melee;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        
        enemyWeapon = GetComponentInChildren<EnemyWeapon>();

        if (enemyWeapon.enabled)
        {
            melee = false;
            weaponRange = enemyWeapon.range;
            enemyWeapon.gameObject.SetActive(false);
        }
        else melee = true;

        facingRight = true;
    }

    //----------------------------------------------------------

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
            rb.drag = 5000f;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
            rb.drag = 0;
    }

    //----------------------------------------------------------

    void Update()
    {
        if (GetComponent<EnemyPatrol>().enemyState == 1)
        {
            animator.SetInteger("state", 1);
            return;
        }

        animator.SetInteger("state", 2);

          if(melee)
           {
                meleeAttack();
                return;
           }
       
        attack();//atac pistol
        

        //----------------------------------------------------------

        hit = Physics2D.Raycast(transform.position + 
            new Vector3(checkPitDist, 0), 
            Vector2.down, 2f, whatIsGround);

        Debug.DrawRay(transform.position + new Vector3(checkPitDist, 0), Vector2.down, Color.green);

        if (hit.collider==null && grounded())
        {
            jump();
            return;
        }
        return;
    }

    //----------------------------------------------------------

    public void meleeAttack()
    {
        checkFlip();   

        if (Vector2.Distance(transform.position, player.transform.position) >= meleeRange) //merg spre player
        {
            if (!coroutineStarted)
                StartCoroutine(UpdatePlayerPos());

            animator.SetBool("inmeleerange", false);

                transform.position = Vector3.MoveTowards(
                    transform.position, 
                    playerPos,
                    enemySpeed * Time.deltaTime);
        }

        //----------------------------------------------------------

        else if (Vector2.Distance(transform.position, player.transform.position) <= meleeRange) //daca playerul e in range, atac melee
        {
            int rng = (RNG.Rng() % 2) + 1;
            animator.SetBool("inmeleerange", true);
            if (Time.time > (lastAttackTime + rng * 1.0) * meleeDelay)
            {
                int damageDone = ((RNG.Rng() % 5) + 1) * baseDamage;
                animator.SetInteger("punchanimation", rng);
                animator.SetTrigger("ispunching");

                player.GetComponent<PlayerHealth>().TakeDamage(damageDone);
                Debug.Log("Taken " + damageDone + " damage");
                lastAttackTime = Time.time;
            }
        }        
    }

    void attack()
    {
        //----------------------------------------------------------------

        checkFlip();

        if (Vector2.Distance(transform.position, player.transform.position) >= weaponRange) //merg spre player
        {
            animator.SetBool("aiming", false);
            enemyWeapon.gameObject.SetActive(false);

            if (!coroutineStarted)
                StartCoroutine(UpdatePlayerPos());

            transform.position = Vector3.MoveTowards(transform.position, 
                playerPos, enemySpeed*Time.deltaTime);
        }

        //----------------------------------------------------------------

        else if (Vector2.Distance(transform.position, player.transform.position) <= weaponRange) //daca playerul e in range, atac cu pistolul
        {
            animator.SetBool("aiming", true);
            enemyWeapon.gameObject.SetActive(true);

            if (Time.time > lastAttackTime + ((RNG.Rng() % 2) + 1 * 1.0) * enemyWeapon.fireRate)
            {
                float dir;
                if (facingRight)
                    dir = 1;
                else dir = -1;
                enemyWeapon.Shoot(dir);

                lastAttackTime = Time.time;
            }        
        }
    }

    //----------------------------------------------------------------

    void checkFlip()
    {    

        if (player.transform.position.x - transform.position.x > 0
            && !facingRight)
        {
            flip(true);
        }

        else if (player.transform.position.x - transform.position.x < 0 
            && facingRight)
        {
            flip(false);
        }

    }

    //----------------------------------------------------------------

    public void flip(bool flipRight)
    {
        Vector3 newScale = transform.localScale;
        newScale.x = flipRight == true ? Mathf.Abs(newScale.x) : Mathf.Abs(newScale.x) * -1;
        transform.localScale = newScale;
        facingRight = flipRight;
    }

    //----------------------------------------------------------------

    IEnumerator UpdatePlayerPos()
    {
        coroutineStarted = true;
        if (player != null)
            playerPos = player.position;
        yield return new WaitForSeconds(1f / updateRate);
        
        StartCoroutine(UpdatePlayerPos());
    }

    //----------------------------------------------------------

    void jump()
    {
        RaycastHit2D nextPlatform = 
            Physics2D.Raycast(transform.position + 
            new Vector3(jumpDistance,0), 
            Vector2.down, 5f, whatIsGround);

        if (nextPlatform.collider == null)
            return;

        Debug.Log(nextPlatform.collider.name);

        float distance = Vector2.Distance(nextPlatform.point, hit.point);

        rb.AddForce(new Vector2(distance / 2, distance));

    }

    //----------------------------------------------------------

    bool grounded()
    {
        RaycastHit2D hit2D =
            Physics2D.Raycast(transform.position, Vector2.down, 2f, whatIsGround);

        if (hit2D.collider != null)
            return true;
        return false;
    }    

}
