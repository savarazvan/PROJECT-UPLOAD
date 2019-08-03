using UnityEngine;

public class RocketLauncher : MonoBehaviour
{
    public GameObject rocketExplosion;
    public float explosionForce, explosionRadius, damage;
    Rigidbody2D rb;
    GameObject player;
    float launchTime;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        launchTime = Time.time;
    }
    

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (Time.time > launchTime + 0.1f)
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
        {
            Rigidbody2D rb = nearbyObjects.GetComponent<Rigidbody2D>();

            EnemyHealth enemy = nearbyObjects.GetComponent<EnemyHealth>();

            if (rb != null)
            {
                ExplosionForce2D.AddExplosionForce(rb, explosionForce * 100, transform.position, explosionRadius);
                if (rb.gameObject.tag == "Player")
                {
                    float proximity = (transform.position - rb.transform.position).magnitude;
                    float effect = 1 - proximity / explosionRadius;
                    rb.GetComponent<PlayerHealth>().TakeDamage(25 * Mathf.RoundToInt(effect));
                }
            }
        
        }

    }
}


