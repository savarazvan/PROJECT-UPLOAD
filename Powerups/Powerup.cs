using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New powerup", menuName = "Powerup")]
public class Powerup : ScriptableObject
{
    public new string name;
    public Sprite sprite;
    public float duration;
    public int id;
}

