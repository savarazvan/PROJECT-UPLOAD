using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;

    public void spawn(int health)
    {
        var spawned = Instantiate(enemy, transform.position, transform.rotation, transform);
        spawned.GetComponent<EnemyHealth>().currentHealth = health;
        StartCoroutine(spawned.GetComponent<EnemyPatrol>().GetComponentInChildren<EnemyFieldOfView>().react());      
    }
}
