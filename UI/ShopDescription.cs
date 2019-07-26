using UnityEngine;
using UnityEngine.UI;

public class ShopDescription : MonoBehaviour
{
    public EndlessArenaManager endless;
    public UpgradeMenuPointer shopPointer;
    public Text title, description, requirement;
    public Image circularProgressBar;
    public Text circularProgresBarNumber;
    int x, y;


    void text(string TitleText, string DescriptionText, string RequirementText)
    {
        title.text = TitleText;
        description.text = DescriptionText;
        requirement.text = RequirementText;
    }

    // Update is called once per frame
    void Update()
    {
        x = shopPointer.x;
        y = shopPointer.y;

        circularProgresBarNumber.text = circularProgressBar.fillAmount < 1 ? EndlessArenaManager.score + "p" : "BUY!";
        if (x == 1)
        {
            if (y == 1)
            {
                text("",
                    "",
                    "");

                circularProgressBar.fillAmount = (float)EndlessArenaManager.score / 500;

            }
            else if (y == 2)
            {
                text("",
                 "",
                 "");

                circularProgressBar.fillAmount = (float)EndlessArenaManager.score / 3000;
            }
            else if (y == 3)
            {
                text("",
                "",
                "");

                circularProgressBar.fillAmount = (float)EndlessArenaManager.score / 7000;
            }
        }

        else if (x == 2)
        {
            if (y == 1)
            {
                text("",
                    "",
                    "");

                circularProgressBar.fillAmount = (float)EndlessArenaManager.score / 5000;
            }
            else if (y == 2)
            {
                text("",
                    "",
                    "");
                circularProgressBar.fillAmount = (float)EndlessArenaManager.score / 5000;
            }
            else if (y == 3)
            {
                text("",
                    "",
                    "");

                circularProgressBar.fillAmount = (float)EndlessArenaManager.score / 5000;
            }
       }
        else if (x == 3)
        {
            if (y == 1)
            {
                text("",
                      "",
                      "");

                circularProgressBar.fillAmount = (float)EndlessArenaManager.score / 7500;

            }
            else if (y == 2)
            {
                text("",
                   "",
                   "");

                circularProgressBar.fillAmount = (float)EndlessArenaManager.score / 10000;
            }
            else if (y == 3)
            {
                text("",
                   "",
                   "");

                circularProgressBar.fillAmount = (float)EndlessArenaManager.score / 12500;
            }
        }

    }
}
