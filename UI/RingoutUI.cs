using UnityEngine;
using UnityEngine.UI;

public class RingoutUI : MonoBehaviour
{
    public Text p1Score, p2Score;
    public Text whoWonText;
    public GameObject whoWonBackground;
    public GameObject scoreBoard;

    void Start()
    {
        p1Score.text = "" + 0;
        p2Score.text = "" + 0;
    }
    

    void Update()
    {
        if(!RingoutManager.roundOver)
        {
            if (whoWonBackground.activeSelf)
                whoWonBackground.SetActive(false);
            if (!scoreBoard.activeSelf)
                scoreBoard.SetActive(true);
            return;
        }

        int whoWon = RingoutManager.whoWon;

        if (whoWon == 1)
        {
            p1Score.text = "" + RingoutManager.p1score;
        }

        else p2Score.text = "" + RingoutManager.p2score;

        scoreBoard.SetActive(false);
        whoWonBackground.SetActive(true);
        whoWonText.text = "Player " + whoWon + " scores!";
  
    }
}
