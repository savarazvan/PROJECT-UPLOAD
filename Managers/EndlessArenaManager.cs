using System.Collections;
using UnityEngine;

public class EndlessArenaManager : MonoBehaviour
{
    public static int currentWave, combo, score;
    public static float comboTime, nextWaveTime;
    public static bool waveClear;
    public EnemySpawner[] spawnPoints;
    public PlayerInventory inv;
    public PowerupSlot powerup;

    //--------------------------------------------------

    private void Start()
    {
        currentWave = 0;
        combo = 1;
        comboTime = 0f;
        EnemyHealth.OnEnemyDeath += killed;
        spawnPoints = Transform.FindObjectsOfType<EnemySpawner>();
        powerup = inv.GetComponentInParent<PowerupSlot>();
        UpgradeManager.controls.Disable();

        StartCoroutine(nextWave());
    }

    //--------------------------------------------------

    private void Update()
    {

        if (spawnPoints.Length == 0)
        {
            spawnPoints = Transform.FindObjectsOfType<EnemySpawner>();
           
            return;
        }

        if (waveClear)
        {
            nextWaveTime -= Time.deltaTime;
            return;
        }

        if (comboTime > 0 && combo>1)
            comboTime -= Time.deltaTime;
        else
            combo = 1;

    }

    //--------------------------------------------------

    void killed()
    {
        combo++;
        comboTime = 5f;
        score += (50 * currentWave / 2) * combo;

        EnemyAI enemy = Transform.FindObjectOfType<EnemyAI>();
        
        if (enemy!=null)
            return;
    
        StartCoroutine(nextWave());
    }

    //--------------------------------------------------

    IEnumerator nextWave()
    {
        UpgradeManager.controls.Enable();
        nextWaveTime = 7f;
        waveClear = true;
        currentWave++;
        yield return new WaitForSeconds(7f);
        foreach (EnemySpawner spawner in spawnPoints)
        {
            Debug.Log(spawner.gameObject.name);
            spawner.spawn(100);
        }

        UpgradeManager.controls.Disable();
        waveClear = false;
    }

    //--------------------------------------------------

    public int buy(int y, int x, bool unlock)
    {
        Debug.Log("unlock");
        switch(x)
        {
            default:
            {
                    return 0;
            }

            case 0:
                {
                    switch(y)
                    {
                        case 0:
                            {
                                if (unlock)
                                {
                                    if (!inv.unlockedWeapon[1])
                                        inv.unlockedWeapon[1] = true;
                                }
                                return 500;
                            }
                        case 1:
                            {
                                if (unlock)
                                {
                                    if (!inv.unlockedWeapon[2])
                                        inv.unlockedWeapon[2] = true;
                                }
                                return 3000;
                            }
                        case 2:
                            {
                                if (unlock)
                                {
                                    if (inv.unlockedWeapon[3])
                                        inv.unlockedWeapon[3] = true;
                                }
                                return 7000;
                            }
                        default:
                            {
                                return 0;
                            }
                    }
                }

            case 1:
                {
                    switch(y)
                    {
                        case 0:
                            {
                                if (unlock)
                                {
                                    powerup.powerupSlot = powerup.speed;
                                }
                                return 5000;
                            }
                        case 1:
                            {
                                if(unlock)
                                    powerup.powerupSlot = powerup.regeneration;
                                return 5000;
                            }
                        case 2:
                            {
                                if(unlock)
                                    powerup.powerupSlot = powerup.quad;
                                return 5000;
                            }
                        default:
                            {
                                return 0;
                            }
                    }
                }

            case 2:
                {
                    switch(y)
                    {
                        case 0:
                            {
                                return 7500;
                            }
                        case 1:
                            {
                                if(unlock)
                                    CharacterMovement.candoublejump = true;
                                return 10000;
                            }
                        case 2:
                            {
                                if(unlock)
                                    PlayerHealth.maxHealth += 25;
                                return 12500;
                            }
                        default:
                            {
                                return 0;
                            }
                    }
                }
        }
    }

    //--------------------------------------------------

}
