using UnityEngine;

public class ShotgunBullet : MonoBehaviour
{
    public float damage;

    private void Start()
    {
        Destroy(gameObject, 10);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Ground")
        {
            GetComponent<Rigidbody2D>().isKinematic = true;
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            if (collision.gameObject.tag == "Enemy")
            {
                float damageDone = ((RNG.Rng() % 3) + 1) + damage;
                collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(damageDone);


                if (collision.GetComponent<EnemyHealth>().currentHealth <= 0)
                    UpgradeManager.weaponKills++;
                UpgradeManager.pistolKills++;
            }
            Destroy(gameObject);
        }
    }
}
