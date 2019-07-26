using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class apardispar : MonoBehaviour
{
    LineRenderer line;
    BoxCollider2D col;
    float posy;
    bool isDoingStuff;
    public float apardisparTime=3f;
    // Start is called before the first frame update
    void Start()
    {
        line=GetComponent<LineRenderer>();
        col = GetComponent<BoxCollider2D>();
        posy = line.GetPosition(2).y;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDoingStuff)
            StartCoroutine("doStuff");
    }

    IEnumerator doStuff()
    {
        int rng = (RNG.Rng() % 2) + 1;
        isDoingStuff = true;
        col.enabled = !col.enabled;
        line.enabled = !line.enabled;
        yield return new WaitForSeconds(rng * apardisparTime);
        isDoingStuff = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            collision.GetComponent<PlayerHealth>().TakeDamage(50);
        else if (collision.gameObject.tag == "Enemy")
            collision.GetComponent<EnemyHealth>().TakeDamage(50);
    }

}
