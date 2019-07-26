using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;

    public void spawn(int health)
    {
        var spawned = Instantiate(enemy, transform, true);
        spawned.GetComponent<EnemyHealth>().currentHealth = health;
    }
}
