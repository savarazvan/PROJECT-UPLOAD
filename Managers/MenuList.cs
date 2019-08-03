using UnityEngine;

public static class MenuList
{
    public static MenuStruct.Menu mainMenu, campaignSelect, saveSelect, pauseMenu, autosaveSlot, play, settings, pauseSettings, gameOver;
    public static MenuStruct.settingsMenu[] settingsOptions = new MenuStruct.settingsMenu[5];

    public static void InitializeSettingsOptions(int pointer)
    {
        settingsOptions[pointer].optionsName = new string[settingsOptions[pointer].optionsNumber];
    }

    public static void Initialize()
    {
        //------------------MAIN MENU----------------------    

        if (mainMenu.UI == null)
        {
            mainMenu.ID = 0;

            mainMenu.horizontalNav = false;

            mainMenu.rectScale = new Vector2(300f, 74.4f);

            mainMenu.positions = new Vector2[3];

            mainMenu.positions[0] = new Vector2(0, 112);
            mainMenu.positions[1] = new Vector2(0, 0);
            mainMenu.positions[2] = new Vector2(0, -112);

            mainMenu.UI = GameObject.Find("Main");
        }

        //-----------------CAMPAIGN SELECT--------------------

        if (campaignSelect.UI == null)
        {
            campaignSelect.ID = 1;

            campaignSelect.horizontalNav = true;

            campaignSelect.rectScale = new Vector2(300, 300);

            campaignSelect.positions = new Vector2[2];

            campaignSelect.positions[0] = new Vector2(-300, 0);
            campaignSelect.positions[1] = new Vector2(200, 0);

            campaignSelect.UI = GameObject.Find("CampaignSelect");

            campaignSelect.UI.SetActive(false);
        }
    

        //-------------------SAVE SELECT----------------------

        saveSelect.ID = 2;

        saveSelect.rectScale = new Vector2(0, 0);

        saveSelect.positions = new Vector2[1];

        saveSelect.positions[0] = new Vector2(0, 0);

        saveSelect.UI = GameObject.Find("SaveSelect");

        saveSelect.UI.SetActive(false);

        //-------------------PAUSE MENU----------------------

        pauseMenu.ID = 3;

        mainMenu.horizontalNav = false;

        pauseMenu.rectScale = new Vector2(300f, 74.4f);

        pauseMenu.positions = new Vector2[3];

        pauseMenu.positions[0] = new Vector2(0, 112);
        pauseMenu.positions[1] = new Vector2(0, 0);
        pauseMenu.positions[2] = new Vector2(0, -112);

        pauseMenu.UI = GameObject.Find("Pause");        


        //------------------AUTOSAVE SLOT----------------------

        autosaveSlot.ID = 4;
        autosaveSlot.horizontalNav = true;

        autosaveSlot.rectScale = new Vector2(250, 250);

        autosaveSlot.positions = new Vector2[4];

        autosaveSlot.positions[0] = new Vector2(-450, 0);
        autosaveSlot.positions[1] = new Vector2(-150, 0);
        autosaveSlot.positions[2] = new Vector2(150, 0);
        autosaveSlot.positions[3] = new Vector2(450, 0);

        autosaveSlot.UI = GameObject.Find("SelectSlot");

        autosaveSlot.UI.SetActive(false);
        

        //----------------------PLAY--------------------------

        play.ID = 5;
        play.horizontalNav = true;

        play.rectScale = new Vector2(350, 350);

        play.positions = new Vector2[3];

        play.positions[0] = new Vector2(-450, 0);
        play.positions[1] = new Vector2(0, 0);
        play.positions[2] = new Vector2(450, 0);

        play.UI = GameObject.Find("Play");

        play.UI.SetActive(false);

       
        //----------------------SETTINGS--------------------------

        settings.ID = 6;
        settings.horizontalNav = false;

        settings.rectScale = new Vector2(819, 90);

        settings.positions = new Vector2[5];

        settings.positions[0] = new Vector2(0, 183);
        settings.positions[1] = new Vector2(0, 93);
        settings.positions[2] = new Vector2(0, 0);
        settings.positions[3] = new Vector2(0, -93);
        settings.positions[4] = new Vector2(0, -183);

        settings.UI = GameObject.Find("Settings");

        settings.UI.SetActive(false);

        //-------------------PAUSE SETTINGS---------------------

        pauseSettings.ID = 7;
        pauseSettings.horizontalNav = false;

        pauseSettings.rectScale = new Vector2(819, 90);

        pauseSettings.positions = new Vector2[5];

        pauseSettings.positions = settings.positions;

        pauseSettings.UI = GameObject.Find("PauseSettings");

        pauseSettings.UI.SetActive(false);
        

        //-------------------GAME OVER---------------------

        gameOver.ID = 8;
        gameOver.horizontalNav = true;

        gameOver.rectScale = new Vector2(300, 100);

        gameOver.positions = new Vector2[2];

        gameOver.positions[0] = new Vector2(-200, -95);
        gameOver.positions[1] = new Vector2(200, -95);

        gameOver.UI = GameObject.Find("GameOver");

        gameOver.UI.SetActive(false);

        gameOver.UI.transform.parent.gameObject.SetActive(false);

    }

    public static void initializeSettingsOptions()
    {

        //VOLUME
        settingsOptions[0].optionsNumber = 11;
        InitializeSettingsOptions(0);
        for (int i = 0; i < 11; i++)
        {
            settingsOptions[0].optionsName[i] = i * 10 + "";
        }

        //FULLSCREEN
        settingsOptions[1].optionsNumber = 2;
        InitializeSettingsOptions(1);
        settingsOptions[1].optionsName[0] = "Yes";
        settingsOptions[1].optionsName[1] = "No";

        //RESOLUTIONS
        Resolution[] resolutions = Screen.resolutions;
        settingsOptions[2].optionsNumber = resolutions.Length;
        InitializeSettingsOptions(2);
        for (int i = 0; i < settingsOptions[2].optionsNumber; i++)
        {
            settingsOptions[2].optionsName[i] = resolutions[i].width
                + " x " + resolutions[i].height;
        }

        //QUALITY
        settingsOptions[3].optionsNumber = 6;
        InitializeSettingsOptions(3);
        for (int i = 0; i < 6; i++)
        {
            settingsOptions[3].optionsName[i] = QualitySettings.names[i];
        }

        //AUTOSAVE
        settingsOptions[4].optionsNumber = 2;
        InitializeSettingsOptions(4);
        settingsOptions[4].optionsName[0] = "Yes";
        settingsOptions[4].optionsName[1] = "No";

    }
}
