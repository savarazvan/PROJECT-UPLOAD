using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static int maxHealth;
    public static int currentHealth;
    public GameObject gameOverUI;
    Animator animator;

    void Start()
    {
        maxHealth = 100;
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }
    
    void Update()
    { 
        if (currentHealth <= 0)
            StartCoroutine(gameoverScreen());
    }

    public void TakeDamage(int ammount)
    { currentHealth -= ammount; }
    IEnumerator gameoverScreen()
    {
        animator.SetTrigger("die");
        yield return new WaitForSeconds(0.8f);
       // gameOverUI.active = true;
    }
 
}
