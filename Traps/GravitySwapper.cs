using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySwapper : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            swapGravity(collision.gameObject);
    }

    void swapGravity(GameObject col)
    {
        Rigidbody2D rb;
        Transform transform;
        CharacterMovement characterMovement;
        rb = col.GetComponent<Rigidbody2D>();
        transform = col.GetComponent<Transform>();
        characterMovement = col.GetComponent<CharacterMovement>();
        rb.gravityScale *= -1;
        characterMovement.jumpForce *= -1;
        transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y * -1);
        if (rb.gravityScale < 0)
        {
           
           
            characterMovement.groundDir = Vector2.up;
            
        }
        else if (rb.gravityScale > 0)
        {

            characterMovement.groundDir = Vector2.down;
        }
    }
}
