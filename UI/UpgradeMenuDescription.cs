using UnityEngine;
using UnityEngine.UI;
public class UpgradeMenuDescription : MonoBehaviour
{
    //---------------------------------------------------------

    public UpgradeMenuPointer upgradeMenuPointer;
    public Text title, description, requirement;
    public Image circularProgressBar;
    public Text circularProgresBarNumber;
    int x, y, Xprev = -1, Yprev = -1;
    private string[,] titles = new string[3, 3];
    private string[,] descs = new string[3, 3];
    private string[,] reqs = new string[3, 3];
    public string[] descriptions = new string[9];
    PlayerInput controls;
    public EndlessArenaManager endless;

    //---------------------------------------------------------

    void Start()
    {
        int X = 0, Y = 0;

        foreach (string str in descriptions)
        {
            string[] tempSplit = str.Split('/');
            if (Y >= 3)
            {
                X++;
                Y = 0;
            }

            titles[Y, X] = tempSplit[0];
            descs[Y, X] = tempSplit[1];
            reqs[Y, X] = tempSplit[2];

            Y++;

        }
        
             
    }

    //---------------------------------------------------------

    void Update()
    {
        x = upgradeMenuPointer.x;
        y = upgradeMenuPointer.y;

        if (Xprev == x && Yprev == y)
            return;

        //---------------------------------------------------------

        title.text = titles[x, y];
        description.text = descs[x, y];
        requirement.text = reqs[x, y];

        Xprev = x;
        Yprev = y;

        //---------------------------------------------------------

        if(GameMaster.gameMode==2)
        {
            circularProgressBar.fillAmount = (float)EndlessArenaManager.score / endless.buy(x, y, false);
            circularProgresBarNumber.text = EndlessArenaManager.score+"p/ " +  endless.buy(x,y,false)+"p";
            return;
        }

        //--------------------------------------------------------- 

        switch (x)
        {
            case 0:
                {
                    switch (y)
                    {
                        case 0:
                            {
                                circularProgressBar.fillAmount = (float)UpgradeManager.enemyKills / 10;
                                circularProgresBarNumber.text = UpgradeManager.enemyKills + "/10";
                                break;
                            }
                        case 1:
                            {
                                circularProgressBar.fillAmount = (float)UpgradeManager.meleeKills / 5;
                                circularProgresBarNumber.text = UpgradeManager.meleeKills + "/5";
                                break;
                            }
                        case 2:
                            {
                                circularProgressBar.fillAmount = (float)UpgradeManager.powerupUses / 5;
                                circularProgresBarNumber.text = UpgradeManager.powerupUses + "/5";
                                break;
                            }
                    }
                    break;
                }

            case 1:
                {
                    switch (y)
                    {
                        case 0:
                            {
                                circularProgressBar.fillAmount = (float)UpgradeManager.weaponKills / 5;
                                circularProgresBarNumber.text = UpgradeManager.weaponKills + "/5";
                                break;
                            }
                        case 1:
                            {
                                circularProgressBar.fillAmount = (float)UpgradeManager.pistolKills / 5;
                                circularProgresBarNumber.text = UpgradeManager.pistolKills + "/5";
                                break;
                            }
                        case 2:
                            {
                                circularProgressBar.fillAmount = (float)UpgradeManager.weaponKills / 15;
                                circularProgresBarNumber.text = UpgradeManager.weaponKills + "/15";
                                break;
                            }
                    }
                    break;
                }
            case 2:
                {
                    switch (y)
                    {
                        case 0:
                            {
                                circularProgressBar.fillAmount = (float)UpgradeManager.jumpTimes / 20;
                                circularProgresBarNumber.text = UpgradeManager.jumpTimes + "/20";
                                break;
                            }
                        case 1:
                            {
                                circularProgressBar.fillAmount = (float)UpgradeManager.speedUses / 10;
                                circularProgresBarNumber.text = UpgradeManager.speedUses + "/10";
                                break;
                            }
                        case 2:
                            {
                                circularProgressBar.fillAmount = (float)UpgradeManager.speedUses / 10;
                                circularProgresBarNumber.text = UpgradeManager.speedUses + "/10";
                                break;
                            }
                    }
                    break;
                }
        }


       

    }

}

