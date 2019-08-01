using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Text[] options;
    PlayerInput controls;
    public MenuCursor cursor;
    public Image cursor2;
    public static int[] settings;

    private void Awake()
    {
        controls = new PlayerInput();
        controls.Menu.Navigate.performed += ctx => changeSettings(ctx.ReadValue<Vector2>());

    }

    void Start()
    {
        MenuList.initializeSettingsOptions();   

        options = GetComponentsInChildren<Text>();

        loadSettings(true);

        Debug.Log(settings.Length);
    }

   
    void changeSettings(Vector2 nav)
    {
        if (nav.x == 0)
            return;
        int p = cursor.pointerPos;

        //------------------------------------------------------------------

        if (settings == null)
        {
            Debug.LogError("Null array!");
            return;
        }

        if (settings[p] == 0 && nav.x < 0)
        {
            settings[p] = MenuList.settingsOptions[p].optionsNumber-1;
        }

        else if (settings[p] == MenuList.settingsOptions[p].optionsNumber - 1 && nav.x > 0)
        {
            settings[p] = 0;
        }

        else
        {
            settings[p] = nav.x > 0 ? settings[p] + 1 :
                settings[p] - 1;
        }

        //----------------------------------------------------------------------

        SettingsManager.applySettings(p, settings[p]);

        options[p].text = MenuList.settingsOptions[p].optionsName[settings[p]];
    }

    void loadSettings(bool applySettings)
    {
        cursor = GameObject.FindObjectOfType<MenuCursor>();

        settings = SettingsManager.loadSettings();
        Debug.Log("Loading settings");

        if (settings == null)
        {
            settings = SettingsManager.defaultSettings();
            Debug.Log("Loading default settings");
        }

        for (int i = 0; i < MenuList.settingsOptions.Length; i++)
        {
            options[i].text = MenuList.settingsOptions[i].optionsName[settings[i]];

            if(applySettings)
                SettingsManager.applySettings(i, settings[i]);
        }

    }


    private void OnEnable()
    {
        loadSettings(false);
        controls.Enable();
    }

    private void OnDisable()
    {
        SettingsManager.saveSettings(settings);
        Debug.Log("Settings saved!");
        controls.Disable();
    }
}
