using UnityEngine;
using UnityEngine.InputSystem;

public class PowerupManager 
{
    
    public void activate(int id)
    {
        switch (id)
        {
            //-------------------------------SPEED-------------------------------------
            case 1:
                {
                    CharacterMovement.moveSpeed = 10f;
                    break;
                }

            //----------------------------REGENERATION----------------------------------

            case 2:
                 {
                   if(Time.time>=regenLast+regenFreq)
                    {
                        PlayerHealth.currentHealth += 10;
                        regenLast = Time.time;
                    }
                    break;
                 }
            //-----------------------------TIME SHIFT---------------------------------
            case 3:
                {
                    Time.timeScale = 0.05f;
                    Time.fixedDeltaTime = 0.02f * Time.timeScale;

                    if (keyboard.aKey.wasPressedThisFrame || 
                        keyboard.dKey.wasPressedThisFrame ||
                        keyboard.spaceKey.wasPressedThisFrame)
                    {
                        Time.timeScale = 1f;
                        Time.fixedDeltaTime = 0.02f;
                    }

                    else if(keyboard.aKey.wasReleasedThisFrame ||
                            keyboard.dKey.wasReleasedThisFrame||
                            keyboard.spaceKey.wasReleasedThisFrame)
                    {
                        Time.timeScale = 0.05f;
                        Time.fixedDeltaTime = 0.02f * Time.timeScale;
                    }
                    if (powerupSlot.powerupTimer <= 0f)
                       {
                        Time.timeScale = 1f;
                        Time.fixedDeltaTime = 0.02f;
                        powerupSlot.powerupInCooldown = true;
                       }                      
                    
                    break;
                }
            //------------------------TREATH TRACK-------------------------------
            case 4:
                {
                    if(playerPos==null)
                        playerPos = GameObject.Find("Player").GetComponent<Transform>();
                    float minDistance = 100f;
                    foreach (Transform enemy in GameMaster.currentRoom.GetComponent<Transform>())
                    {
                        if (enemy.gameObject.tag == "Enemy")
                        {
                            float currentDistance = Vector2.Distance(enemy.GetComponent<Transform>().position, playerPos.position);
                            if (currentDistance < minDistance)
                            {
                                minDistance = currentDistance;
                                enemyLocation = enemy.GetComponent<Transform>().position;
                            }
                        }
                    }

                    if (Time.time < lastTrack + 2)
                        return;

                    var treath = GameObject.Instantiate(treathTrack);
                    treath.GetComponent<Transform>().parent = GameMaster.currentRoom.GetComponent<Transform>();
                    treath.GetComponent<Transform>().position = enemyLocation;
                    GameObject.Destroy(treath, 2f);
                    lastTrack = Time.time;
                    break;

                }
                //----------------------------------------------------------------
                   
            default:
                {
                    Debug.Log("Powerup ID: " + id + " does not exist");
                    break;
                }
        }
    }

    //----------EXTRA VARIABLES------------

    float regenFreq=2f, regenLast;
    float initialSpeed;
    float lastTrack;
    GameObject treathTrack;
    Transform playerPos;
    Vector2 enemyLocation;
    PowerupSlot powerupSlot;
    Keyboard keyboard;
    Gamepad gamepad;


    //-------------------------------------

    public void loadResources()
    {
        treathTrack = Resources.Load<GameObject>("Powerups/Particles/TreathTrack");
        playerPos = GameObject.Find("Player").GetComponent<Transform>();
        powerupSlot = playerPos.GetComponent<PowerupSlot>();
        keyboard = Keyboard.current;
        gamepad = Gamepad.current;
    }
}
