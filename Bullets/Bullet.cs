using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rb;
    public float baseDamage;
   
    private void Start()
    {
        Destroy(gameObject, 10);
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Ground") 
        {
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;
            if (collision.gameObject.tag == "Enemy")
            {
                float damageDone = ((RNG.Rng() % 3) + 1) * baseDamage;
                collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(damageDone);
                

                if (collision.GetComponent<EnemyHealth>().currentHealth <= 0)
                    UpgradeManager.weaponKills++;
                    UpgradeManager.pistolKills++;
            }
            Destroy(gameObject);
        }
    }
   
    private void OnParticleCollision(GameObject other)
    {
        if(other.gameObject.tag!="Player")
        Destroy(gameObject);
    }

    
}
