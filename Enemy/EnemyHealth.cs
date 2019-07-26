using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth;
    public float currentHealth;
    public bool isDead;
    public static event Action OnEnemyDeath;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            if(UpgradeManager.enemyKills<10)
            UpgradeManager.enemyKills++;
            Destroy(gameObject);
        }
    }
    public void TakeDamage(float ammount)
    { currentHealth -= ammount; }
    private void OnDestroy()
    {
        if (OnEnemyDeath != null)
            OnEnemyDeath();
    }
}
