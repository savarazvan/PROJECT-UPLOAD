using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [HideInInspector] public int damageDone;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.GetComponent<PlayerHealth>().TakeDamage(damageDone);
            Destroy(gameObject);
        }
       
    }
}
