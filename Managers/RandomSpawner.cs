using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public float baseInterval;
    public GameObject[] objectsToSpawn;
    public float lastSpawnTime;
    void Start()
    {
        spawnObj((RNG.Rng() % objectsToSpawn.Length ));
        lastSpawnTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= (((RNG.Rng() % 5) + 1) * baseInterval) + lastSpawnTime)//daca a trecut baseInterval inmultit cu functia rng secunde de la ultimul spawn
        {
            spawnObj(RNG.Rng() % objectsToSpawn.Length);//spawnez un obiect dintr-un sir de obiecte, decis de rng
            lastSpawnTime = Time.time;
        }
    }
    void spawnObj(int objnumber)
    {
        var spawnedObject = Instantiate(objectsToSpawn[objnumber]);
        spawnedObject.transform.parent = transform;
        spawnedObject.transform.position = transform.position;
        if (spawnedObject.gameObject.tag == "Enemy")
        {
            objectsToSpawn[objnumber].gameObject.GetComponent<EnemyPatrol>().enemyState = 2;
            objectsToSpawn[objnumber].gameObject.GetComponent<EnemyAI>().player = GameObject.FindWithTag("Player").GetComponent<Transform>();

        }
        else
            Destroy(spawnedObject, 10f);
    }
}
