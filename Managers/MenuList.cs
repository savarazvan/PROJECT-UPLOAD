using UnityEngine;

public static class MenuList
{
    public static MenuStruct.Menu mainMenu, campaignSelect, saveSelect;

    public static void Initialize()
    {
        //------------------MAIN MENU----------------------    

        mainMenu.ID = 0;

        mainMenu.horizontalNav = false;

        mainMenu.rectScale = new Vector2(300f, 74.4f);

        mainMenu.positions = new Vector2[3];

        mainMenu.positions[0] = new Vector2(0, 112);
        mainMenu.positions[1] = new Vector2(0, 0);
        mainMenu.positions[2] = new Vector2(0, -112);

        mainMenu.UI = GameObject.Find("Main");

        //-----------------CAMPAIGN SELECT--------------------

        campaignSelect.ID = 1;

        campaignSelect.horizontalNav = true;

        campaignSelect.rectScale = new Vector2(300, 300);

        campaignSelect.positions = new Vector2[2];

        campaignSelect.positions[0] = new Vector2(-300, 0);
        campaignSelect.positions[1] = new Vector2(200, 0);

        campaignSelect.UI = GameObject.Find("CampaignSelect");

        campaignSelect.UI.SetActive(false);

        //-------------------SAVE SELECT----------------------

        saveSelect.ID = 2;

        saveSelect.rectScale = new Vector2(0, 0);

        saveSelect.positions = new Vector2[1];

        saveSelect.positions[0] = new Vector2(0, 0);

        saveSelect.UI = GameObject.Find("SaveSelect");

        saveSelect.UI.SetActive(false);
    }
}
