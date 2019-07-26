using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargedShot : MonoBehaviour
{
    public GameObject chargedBulletPrefab;
    GameObject canon;
    Transform Arm;
    public float baseSpeed;
    public Slider chargeUI;
    [HideInInspector] public float heldTime;

    bool playerFacingRight()
    {
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMovement>().facingRight)
            return true;
        return false;
    }

    void Start()
    {
        heldTime = 0f;//cat timp am tinut spacebar
        canon = gameObject;
        Arm = gameObject.GetComponentInParent<Transform>();
        chargeUI.value = 0;
        chargeUI.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            heldTime += Time.deltaTime;
            if (heldTime >= 0.5f)
            {
                chargeUI.gameObject.SetActive(true);
                chargeUI.value += (float)0.3 * Time.deltaTime * 4;
            }
        }
        if (Input.GetKeyUp(KeyCode.Space) && heldTime >= 0.5f)
        {
            ChargedShoot(chargeUI.value);
            heldTime = 0f;
            chargeUI.value = 0f;
            chargeUI.gameObject.SetActive(false);
        }
    }
    void ChargedShoot(float damageMultiplier)
    {
        if (damageMultiplier > 3)
            damageMultiplier = 3;
        float speedPenaltyPercent = (int)((damageMultiplier / 3) * 100);
        Debug.Log(speedPenaltyPercent);
        var bullet = Instantiate(chargedBulletPrefab, canon.transform.position, Arm.rotation).GetComponent<Rigidbody2D>();
        if (playerFacingRight())
            bullet.AddForce(canon.transform.right * baseSpeed * Time.deltaTime * 60, ForceMode2D.Impulse);
        else if (!playerFacingRight())
        {
            bullet.AddForce(canon.transform.right * -baseSpeed * Time.deltaTime * 60, ForceMode2D.Impulse);
            bullet.transform.rotation = new Quaternion(0, 0, 180,0);
        }
        Debug.Log(speedPenaltyPercent/100);
        bullet.GetComponent<Bullet>().baseDamage = bullet.GetComponent<Bullet>().baseDamage * damageMultiplier;
        bullet.transform.localScale += new Vector3(damageMultiplier, damageMultiplier, 0);
        Destroy(bullet, 10);
    }
}
