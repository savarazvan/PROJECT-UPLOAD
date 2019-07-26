using UnityEngine;
using UnityEngine.UI;

public class EndlessArenaUI : MonoBehaviour
{
    public Text score, combo, wave;
    public Image comboBar;
    int prevScore;
    private float comboBarSize;

    private void Update()
    {
        if (prevScore != EndlessArenaManager.score)
            prevScore = prevScore < EndlessArenaManager.score ? prevScore + 1 : prevScore - 1;

        score.text = "" + prevScore;
        combo.text = "x" + EndlessArenaManager.combo;

        if(EndlessArenaManager.waveClear)
        {
            wave.text = "Next wave in " + (int)EndlessArenaManager.nextWaveTime;
        }

        else
            wave.text = "Wave " + EndlessArenaManager.currentWave;

        comboBarSize = (int)(((EndlessArenaManager.comboTime / 7) * 100) * 170) / 100;

        comboBar.rectTransform.sizeDelta = new Vector2(comboBarSize, comboBar.rectTransform.sizeDelta.y);

    }
}
