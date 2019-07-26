using UnityEngine;

public class WeaponShoot : MonoBehaviour
{
    public void Shoot(GameObject bulletPrefab, float speed, int fRight)
    {
        var bullet = Instantiate(bulletPrefab, transform.position, GetComponentsInParent<Transform>()[1].localRotation).GetComponent<Rigidbody2D>();
            bullet.AddRelativeForce(Vector2.right * speed * Time.deltaTime * 60 * fRight, ForceMode2D.Impulse);
    }
}
