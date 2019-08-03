using UnityEngine;

public class PreShotgunBullet : MonoBehaviour
{
    public GameObject[] bullets;

    private void Start()
    {
        foreach(GameObject bullet in bullets)
        {
            var instantiated = Instantiate(bullet, transform.position, transform.rotation * Quaternion.Euler(0,0,Random.Range(-45, 45)));
            instantiated.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.right * 60 * GameObject.FindObjectOfType<PlayerInventory>().selectedWeapon.bulletSpeed
                * Time.deltaTime, ForceMode2D.Impulse);
        }
        
        Destroy(gameObject, 3);
    }
}
