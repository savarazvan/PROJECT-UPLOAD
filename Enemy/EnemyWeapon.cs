using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public float bulletSpeed = 50f, fireRate = 2, armedDistance = 5f, range = 8.5f;
    public int baseDamage;
    public GameObject enemyBullet;

    Vector2 playerDir;

    public void Shoot(float dir)
    {
        var bullet = Instantiate(enemyBullet, transform.position, transform.rotation).GetComponent<Rigidbody2D>();
        bullet.AddForce(new Vector2(dir * bulletSpeed * Time.deltaTime * 60, 0), ForceMode2D.Impulse);
        bullet.GetComponent<EnemyBullet>().damageDone = ((RNG.Rng() % 3) +1 ) * baseDamage;
        Destroy(bullet.gameObject, 5);
    }
}
