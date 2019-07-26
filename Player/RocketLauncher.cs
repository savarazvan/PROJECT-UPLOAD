using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : MonoBehaviour
{
    public GameObject rocketExplosion;
    public float explosionForce, explosionRadius, damage;
    Rigidbody2D rb;
    GameObject player;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            Explosion();
            Destroy(gameObject);
        }
    }
    void Explosion()
    {
        Instantiate(rocketExplosion, transform.position, transform.rotation);
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach(Collider2D nearbyObjects in objects)
        {Rigidbody2D rb = nearbyObjects.GetComponent<Rigidbody2D>();
         EnemyHealth enemy = nearbyObjects.GetComponent<EnemyHealth>();
            if (rb!=null)
                {
                ExplosionForce2D.AddExplosionForce(rb, explosionForce*100, transform.position, explosionRadius);
                if(rb.gameObject.tag=="Player")
                {
                    float proximity = (transform.position - rb.transform.position).magnitude;
                    float effect = 1 - proximity / explosionRadius;
                    rb.GetComponent<PlayerHealth>().TakeDamage(25*Mathf.RoundToInt(effect));
                }
                }
        

        }


    }
}


