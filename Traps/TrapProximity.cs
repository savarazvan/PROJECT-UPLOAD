using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapProximity : MonoBehaviour
{
    public float retractSpeed = 1.3f;
    public float waitForSecs = 1f;
    public bool isTriggered;
    public bool goToLeft;
    Rigidbody2D rb;
    Vector3 dest, initialPos;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if(goToLeft)
        dest = new Vector3(transform.position.x + 8f*-1, transform.position.y, transform.position.z);
        else
            dest = new Vector3(transform.position.x + 8f, transform.position.y, transform.position.z);
        initialPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isTriggered)
        {
            if (transform.position.x != initialPos.x)
                transform.position = Vector3.Lerp(transform.position, initialPos, retractSpeed * Time.deltaTime);
            return;
        }
        transform.position=Vector3.Lerp(transform.position, dest, retractSpeed*Time.deltaTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine("retract");
        }
    }
    IEnumerator retract()
    { yield return new WaitForSeconds(waitForSecs);
        isTriggered = true;
        yield return new WaitForSeconds(5f);
        isTriggered = false;
    }
}
