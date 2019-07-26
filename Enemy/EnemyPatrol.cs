using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float patrolDistance, speed = 10f;
    EnemyHealth health;
    Rigidbody2D rb;
    public bool facingRight;
    [HideInInspector] public GameObject player;
    Vector2 startingPosition;
    public int enemyState; // 0 = IDLE (EnemyIdle.cs), 1 = PATROL, 2 = ALERTED


    void Start()
    {
        player = GameObject.Find("Player");
        startingPosition = new Vector2(transform.position.x, transform.position.y);
        health = GetComponent<EnemyHealth>();
        rb = GetComponent<Rigidbody2D>();
        facingRight = GetComponent<EnemyAI>().facingRight;
        enemyState = 1;
    }

    void Update()
    {

        if (health.currentHealth != health.maxHealth)
        {
            enemyState = 2;
            return;
        }

        if (enemyState == 1)
        {
            if (Vector2.Distance(startingPosition, transform.position) > patrolDistance)
            {
                facingRight = !facingRight;
                GetComponent<EnemyAI>().flip(facingRight);

            }
            if (facingRight)
                rb.velocity = new Vector2(speed, rb.velocity.y);
            else if (!facingRight)
                rb.velocity = new Vector2(-speed, rb.velocity.y);
        }

    }
    private void flip()
    { Vector3 tempscale = transform.localScale;
        tempscale.x = -tempscale.x;
        transform.localScale = tempscale; }

    
}
