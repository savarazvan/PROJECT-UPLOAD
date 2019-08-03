using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static int maxHealth;
    public static int currentHealth;
    bool dropingHealth;
    public GameObject gameOverUI;
    Animator animator;
    [HideInInspector] public bool dead;

    //-----------------------------------------------

    void Start()
    {
        maxHealth = 100;
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    //-----------------------------------------------

    void Update()
    {
        if (currentHealth <= 0 && !dead)
        {
            StartCoroutine(gameoverScreen());
            dead = true;
        }

        if(currentHealth>maxHealth && !dropingHealth)
        {
            StartCoroutine(Health2Max());
            dropingHealth = true;
        }
    }

    //-----------------------------------------------

    public void TakeDamage(int ammount)
    { currentHealth -= ammount; }
    IEnumerator gameoverScreen()
    {
        animator.SetTrigger("die");
        yield return new WaitForSeconds(1f);
        if(GameMaster.gameMode!=3)
        {
            GameObject.FindObjectOfType<GameMaster>().pauseGame(GetComponent<CharacterMovement>(), false);
            GameObject.FindObjectOfType<MenuCursor>().changeMenu(MenuList.gameOver);
        }
    }

    //-----------------------------------------------

    IEnumerator Health2Max()
    {
        currentHealth -= 1;
        yield return new WaitForSeconds(1.5f);

        if (currentHealth > maxHealth)
            StartCoroutine(Health2Max());

        else dropingHealth = false;
    }
 
}
