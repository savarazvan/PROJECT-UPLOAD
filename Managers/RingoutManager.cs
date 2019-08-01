using System.Collections;
using UnityEngine;

public class RingoutManager : MonoBehaviour
{
    public static int p1score, p2score;
    public static int whoWon;
    public static bool roundOver = false;
    private Vector3[] respawnPos;
    public static Transform instance;

    void Start()
    {
        instance = transform;

        p1score = 0;
        p2score = 0;


        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        
        PlayerHealth.currentHealth = 999999;
    }

    public static void nextRound(int player)
    {
        whoWon = player;
        roundOver = true;

        if (player == 1)
            p1score++;

        else p2score++;
    }

    public IEnumerator slowMo()
    {   
        Time.timeScale = 0.25f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        yield return new WaitForSecondsRealtime(3);

        foreach (Transform player in transform)
        {
            if (player.tag == "Player")
            {

                if (player.name == "Player")
                    player.transform.position = new Vector2(1, 2.3f);

                else player.transform.position = new Vector2(20, 2.3f);
            }
        }

        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f;

        roundOver = false;
    }
    
}
