using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDamage : MonoBehaviour
{
    public float radius;
    private void Start()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius);
    }

}
