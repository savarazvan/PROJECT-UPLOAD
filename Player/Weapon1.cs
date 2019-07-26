using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon1 : MonoBehaviour
{
    public float fireRate = 0, baseDamage = 10, timeToFire = 0, bulletSpeed;
    public GameObject bulletPrefab;
    public CharacterMovement player;
    public Transform Arm;
    public GameObject canon;

    private bool hasChargeShot;

    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<ChargedShot>() != null)
            hasChargeShot = true;
        else if (GetComponent<ChargedShot>() == null)
            hasChargeShot = false;
        Debug.Log("hasChargeShot " + hasChargeShot);
    }

    // Update is called once per frame
    void Update()
    {
        if (fireRate == 0)
        {
            //daca arma curenta are atasat scriptul de charge, atunci aceasta va trage doar cand jucatorul va lasa tasta space
            if (hasChargeShot)
                if (Input.GetKeyUp(KeyCode.Space)&&GetComponent<ChargedShot>().heldTime<0.5f)
                    Shoot();

            else if(!hasChargeShot)
                if (Input.GetKeyDown(KeyCode.Space))
                    Shoot();
        }
        else if (Input.GetKey(KeyCode.Space) && Time.time > timeToFire)
        {
            timeToFire = Time.time + 1 / fireRate;
            Shoot();
        }
    }
    void Shoot()
    {
        var bullet = Instantiate(bulletPrefab, canon.transform.position, Arm.rotation).GetComponent<Rigidbody2D>();
        if (player.facingRight)
            bullet.AddForce(canon.transform.right * bulletSpeed * Time.deltaTime * 60, ForceMode2D.Impulse);
        else if (!player.facingRight)
        {
            bullet.AddForce(-canon.transform.right * bulletSpeed * Time.deltaTime * 60, ForceMode2D.Impulse);
            bullet.transform.rotation = new Quaternion(0, 0, 180, 0);
        }
        Destroy(bullet.gameObject, 10);

    }
   
}


   