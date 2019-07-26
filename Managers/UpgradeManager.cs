using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static int enemyKills=0, meleeKills=0, powerupUses=0, pistolKills=0, weaponKills=0, jumpTimes=0, speedUses=0;
    public static bool doubleJump, maxHealth, speed, powerupCooldown;
    public GameObject UM;
    public PlayerInput controls;

    private void Awake()
    {
        controls = new PlayerInput();
        controls.Menu.UpgradeMenu.performed += ctx => toggleUM();
    }

    // Start is called before the first frame update
    void Start()
    {
        EnemyHealth.OnEnemyDeath += enemykillplus;
    }


    void toggleUM()
    {
        UM.SetActive(!UM.activeSelf);
    }

    void enemykillplus()
    {
        enemyKills++;
    }

    public static void checkUnlocks()
    {
        if (jumpTimes == 20)
            doubleJump = true;
        if (enemyKills == 10 && !maxHealth)
        {
            PlayerHealth.maxHealth += 25;
            PlayerHealth.currentHealth += 25;
            maxHealth = true;
        }
        if(speedUses==10 && !speed)
        {
            CharacterMovement.moveSpeed += 20 / 100 * CharacterMovement.moveSpeed;
            speed = true;
        }
        /*
        if(powerupUses==5 && !powerupCooldown)
        {
            GameMaster.powerupCooldown -= 2;
        }
        */
    }

    private void OnEnable()
    {
        controls.Enable();
    }
    private void OnDisable()
    {
        controls.Disable();
    }

}
