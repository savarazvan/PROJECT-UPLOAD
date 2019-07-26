﻿using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerInventory : MonoBehaviour
{
    Keyboard keyboard = Keyboard.current;

    public static Weapon[] weapon = new Weapon[4];

    public bool[] unlockedWeapon = new bool[weapon.Length];

    public Sprite armSprite;

    public SpriteRenderer weaponSprite;

    public Weapon selectedWeapon;

    public GameObject childObj;

    public InputAction showWheel, attackKey;

    public GameObject weaponWheel;

    private void Awake()
    {
        attackKey.performed += ctx => attack(selectedWeapon.ID);
        showWheel.performed += ctx => displayWeaponWheel(ctx.ReadValue<float>());
        showWheel.canceled += ctx => displayWeaponWheel(ctx.ReadValue<float>());
    }

    private void Start()
    {
        weapon[0] = Resources.Load<Weapon>("Weapons/Melee");
        weapon[1] = Resources.Load<Weapon>("Weapons/Pistol");
        weapon[2] = Resources.Load<Weapon>("Weapons/Rocket Launcher");
        armSprite = Resources.Load<Sprite>("Sprites/arm");
        switchWeapon(0);
    }

    private void Update()
    {
        
    }


    public void switchWeapon(int ID)
    {
        selectedWeapon = weapon[ID];
        updateWeapon();
        
    }

    void updateWeapon()
    {
        if(selectedWeapon.ID==0)
        {
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<ArmRotation>().enabled = false;
            GetComponent<CircleCollider2D>().enabled = true;
            GetComponent<Weapon_Melee>().enabled = true;
            childObj.SetActive(false);
        }

        else if(selectedWeapon.ID!=0)
        {
            GetComponent<SpriteRenderer>().enabled = true;
            GetComponent<ArmRotation>().enabled = true;
            GetComponent<CircleCollider2D>().enabled = false;
            GetComponent<Weapon_Melee>().enabled = false;
            childObj.SetActive(true);
            childObj.GetComponent<SpriteRenderer>().sprite = selectedWeapon.weaponSprite;
        }
    }

    void attack(int ID)
    {
        int rng = (RNG.Rng() % 3) + 1;
        if (ID == 0)
            GetComponent<Weapon_Melee>().Attack(rng);
        else
        {
            int fRight;

            if (GetComponent<ArmRotation>().facingRight)
                fRight = 1;
            else fRight = -1;

            GetComponentInChildren<WeaponShoot>().Shoot(selectedWeapon.bulletPrefab, selectedWeapon.bulletSpeed, fRight);

        }
    }

    void displayWeaponWheel(float value)
    {
        if (value > 0 && !weaponWheel.activeSelf)
        {
            weaponWheel.SetActive(true);
            weaponWheel.GetComponentInChildren<WeaponWheel>().updateInventory(unlockedWeapon);
            Time.timeScale = 0.05f;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
        }
        else
        {
            weaponWheel.SetActive(false);
            Time.timeScale = 1f;
            Time.fixedDeltaTime = 0.02f;
        }

    }

    private void OnEnable()
    {
        attackKey.Enable();
        showWheel.Enable();
    }

    private void OnDisable()
    {
        attackKey.Disable();
        showWheel.Disable();
    }



}