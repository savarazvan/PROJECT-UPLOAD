using System.Collections;
using UnityEngine;

public class Weapon_Melee : MonoBehaviour
{
    public int baseDamage;
    public Animator animator;
    [HideInInspector] public float meleeRange;
    Collider2D enemy;

   

    public void Attack(int rng)
    {
       Debug.Log(rng); 
       animator.SetInteger("attackanimation", rng);
       animator.SetTrigger("attacking");
        if (enemy == null)
            return;
        StartCoroutine(coroutine(enemy, rng));
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
            enemy = collision;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
            enemy = null;
    }

    //-------------------------------------------------------

    IEnumerator coroutine(Collider2D collision, int rng)
    {
        int damageDone = rng * baseDamage;

        Debug.Log("Done" + damageDone + "damage");

        if (collision.gameObject.GetComponent<EnemyPatrol>().enemyState == 1)
            collision.gameObject.GetComponent<EnemyHealth>().currentHealth = 0;
        else collision.gameObject.GetComponent<EnemyHealth>().currentHealth -= damageDone;

        Time.timeScale = 1.7f;
        yield return new WaitForSeconds(0.07f*Time.timeScale);
        Time.timeScale = 1f;
    }

    //-------------------------------------------------------

   
}

 
