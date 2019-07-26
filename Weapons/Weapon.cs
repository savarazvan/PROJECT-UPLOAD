using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New weapon", menuName = "Weapon")]
public class Weapon : ScriptableObject
{
    public int ID;
    public bool unlocked;
    public int fireRate;
    public GameObject bulletPrefab;
    public Sprite weaponSprite;
    public Animator weaponAnimator;
    public int maxBullets;
    public int currentBullets;
    public float bulletSpeed;
}

