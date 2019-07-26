using UnityEngine;

public class RingoutPit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(GetComponentInParent<RingoutManager>().slowMo());

            if (collision.name == "Player")
                RingoutManager.nextRound(2);


            else
                RingoutManager.nextRound(1);
             
        }
    }
}
