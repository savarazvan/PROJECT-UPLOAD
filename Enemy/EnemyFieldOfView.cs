using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFieldOfView : MonoBehaviour
{
    Transform player;
    public Transform enemy;
    public LayerMask whatIsBlock;
    private void Start()
    {
        enemy = GetComponentInParent<Transform>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }


    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            if (inLOS())
            {
                Debug.DrawLine(enemy.transform.position + new Vector3(5, 0, 0), player.transform.position, Color.red);
                gameObject.GetComponentInParent<EnemyPatrol>().enemyState = 2;
                GetComponent<EnemyPatrol>().enabled = false;
            }
    }

    private bool inLOS()
    {
        Vector3 direction = (player.position - enemy.position).normalized;
        float distance = Vector3.Distance(enemy.position, player.position);
        Debug.DrawLine(enemy.transform.position + new Vector3(5, 0, 0), player.transform.position, Color.white);
        if (!Physics2D.Linecast(enemy.transform.position+ new Vector3(5, 0, 0), player.transform.position, whatIsBlock))
            return true;
        return false;
    }
            
    }
