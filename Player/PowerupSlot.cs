using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PowerupSlot : MonoBehaviour
{
    public Powerup speed, timeshift, regeneration, treathTrack, quad;
    public Powerup powerupSlot;
    public bool powerupActive;
    PowerupManager powerupManager = new PowerupManager();
    public bool powerupInCooldown;
    private GameObject powerupHud;
    public float powerupCooldown = 10f;
    public float powerupTimer;
    public Sprite powerupRegeneration, powerupSpeed, powerupTimeShift;

    public PlayerInput controls;

    private void Awake()
    {
        controls = new PlayerInput();
        controls.Player.Powerup.performed += ctx => activate();
    }

    //---------------------------------------------------

    private void Start()
    {
        speed = Resources.Load<Powerup>("Powerups/Speed");
        timeshift = Resources.Load<Powerup>("Powerups/TimeShift");
        regeneration = Resources.Load<Powerup>("Powerups/Regeneration");
        treathTrack = Resources.Load<Powerup>("Powerups/TreathTrack");

        powerupTimer = powerupCooldown;
        powerupHud = GameObject.Find("PowerupSlot");

        powerupManager.loadResources();

        


    }

    //---------------------------------------------------

    public void activate()
    {
        if (powerupSlot && !powerupInCooldown && !powerupActive)
        {
            powerupTimer = powerupSlot.duration;
            powerupActive = true;
        }
    }

    //---------------------------------------------------

    private void Update()
    {
      
        if (powerupActive)
        {
            powerupManager.activate(powerupSlot.id);
            powerupTimer -= Time.deltaTime;
            if (powerupTimer <= 0f)
            {
                powerupActive = false;
                powerupInCooldown = true;
            }
        }

        //---------------------------------------------------

        if (powerupInCooldown)
        {

            powerupTimer += Time.deltaTime;

            if (powerupTimer >= powerupCooldown)
            {
                powerupTimer = powerupCooldown;
                powerupInCooldown = false;
            }
        }

    }

    //---------------------------------------------------

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }


}
