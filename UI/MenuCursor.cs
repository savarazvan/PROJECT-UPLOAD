using UnityEngine;

public class MenuCursor : MonoBehaviour
{
    public PlayerInput controls;
    public MenuStruct.Menu currentMenu;
    RectTransform rectTransform;
    public GameObject menuUI, nextUI;
    public int pointerPos;
    GameMaster gameMaster;


    private void Awake()
    {
        controls = new PlayerInput();
        controls.Menu.Navigate.performed += ctx => navigate(ctx.ReadValue<Vector2>());
        controls.Menu.Select.performed += ctx => Select(currentMenu.ID, pointerPos, false);
        controls.Menu.Back.performed += ctx => Select(currentMenu.ID, pointerPos, true);
    }

    private void Start()
    {

        rectTransform = GetComponent<RectTransform>();
        MenuList.Initialize();

        currentMenu = MenuList.mainMenu;
        menuUI = GameObject.Find("Main");
        
        gameMaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        
    }

    //---------------------------------------------------------------------

    private void Update()
    {

        if (pointerPos >= currentMenu.positions.Length)
            pointerPos = 0;

        else if (pointerPos < 0)
            pointerPos = currentMenu.positions.Length - 1;

        //---------------------------------------------------------------------

        rectTransform.localPosition = Vector2.Lerp(rectTransform.localPosition,
            currentMenu.positions[pointerPos], 10f * Time.fixedDeltaTime);

        if (rectTransform.sizeDelta != currentMenu.rectScale)
            rectTransform.sizeDelta = Vector3.Lerp(rectTransform.sizeDelta,
                currentMenu.rectScale, 10f * Time.fixedDeltaTime);


    }

    //---------------------------------------------------------------------

    void navigate(Vector2 dir)
    {

        GameObject.FindObjectOfType<AudioManager>().play("Navigate");

        if (!currentMenu.horizontalNav)
        {
            if (dir.y < 0)
                pointerPos++;
            else if (dir.y > 0)
                pointerPos--;
        }

        else if (currentMenu.horizontalNav)
        {
            if (dir.x > 0)
                pointerPos++;
            else if (dir.x < 0)
                pointerPos--;
        }
    }

    //---------------------------------------------------------------------

    void Select(int ID, int pos, bool goBack)
    {

        GameObject.FindObjectOfType<AudioManager>().play("Select");

        switch(ID)
        {

            //------------------MAIN MENU----------------------   

            case 0:
                {
                    if (!goBack)
                    {
                        switch (pos)
                        {
                            case 0:
                                {
                                    changeMenu(MenuList.play);
                                    break;
                                }
                            case 1:
                                {
                     
                                    changeMenu(MenuList.settings);
                                    break;

                                }
                            case 2:
                                {
                                    Application.Quit();
                                    break;
                                }
                        }
                    }
                    break;
                }

            //-----------------CAMPAIGN SELECT--------------------

            case 1:
                {
                    if(goBack)
                        changeMenu(MenuList.play);
                    else
                    {
                        switch (pos)
                        {
                            case 0:
                                {
                                    changeMenu(MenuList.saveSelect);
                                    break;
                                }
                            case 1:
                                {
                                    gameMaster.setGameMode(1);
                                    GetComponentsInParent<Transform>()[1].gameObject.SetActive(false);
                                    GameMaster.currentRoom = GameObject.Find("R1");
                                    break;
                                }
                        }
                    }
                    break;
                }

            //-------------------SAVE SELECT----------------------

            case 2:
                {
                    if (goBack)
                        changeMenu(MenuList.campaignSelect);
                    break;
                }

            //-------------------PAUSE MENU----------------------

            case 3:
                {
                    if (goBack)
                    {
                        transform.parent.gameObject.SetActive(false);
                        GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMovement>().OnEnable();
                        Time.timeScale = 1f;
                        return;
                    }

                    switch (pos)
                    {
                        case 0:
                            {
                                transform.parent.gameObject.SetActive(false);
                                GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMovement>().OnEnable();
                                Time.timeScale = 1f;
                                return;
                            }

                        case 1:
                            {
                                changeMenu(MenuList.pauseSettings);
                                return;
                            }

                        case 2:
                            {
                                gameMaster.setGameMode(0);
                                transform.parent.gameObject.SetActive(false);
                                return;
                            }
                            
                    }

                    break;
                }

            //-------------------AUTOSAVE SLOT----------------------

            case 4:
                {

                    Transform player = GameObject.FindGameObjectWithTag("Player").transform;

                    if (goBack)
                    {
                        player.GetComponent<CharacterMovement>().OnEnable();
                        Time.timeScale = 1;
                        transform.parent.gameObject.SetActive(false);
                        return;
                    }

                    LoadManager.currentSlot = pos + 1;                
                    if (pos != 3)
                        SaveSystem.SavePlayer(player, pos + 1, player.GetComponent<PowerupSlot>().powerupSlot);

                    player.GetComponent<CharacterMovement>().OnEnable();
                    Time.timeScale = 1;
                    changeMenu(MenuList.pauseMenu);
                    transform.parent.gameObject.SetActive(false);
                    return;
                }

            //-------------------PLAY----------------------

            case 5:
                {
                    if(goBack)
                    {
                        changeMenu(MenuList.mainMenu);
                        return;
                    }

                    switch(pos)
                    {
                        case 0:
                            {
                                changeMenu(MenuList.campaignSelect);
                                return;
                            }

                        default:
                            {
                                gameMaster.setGameMode(pos + 1);
                                transform.parent.gameObject.SetActive(false);
                                return;
                            }
                    }
                    
                }

            //-------------------SETTINGS----------------------

            case 6:
                {
                    if(goBack)
                    {
                        changeMenu(MenuList.mainMenu);
                        return;
                    }
                    break;
                }

            //------------------PAUSE SETTINGS----------------------

            case 7:
                {
                    if (goBack)
                    {
                        changeMenu(MenuList.pauseMenu);
                        return;
                    }
                    break;
                }
        }
    }

    //---------------------------------------------------------------------

    public void changeMenu(MenuStruct.Menu menu)
    {
        currentMenu.UI.SetActive(false);
        currentMenu = menu;
        currentMenu.UI.SetActive(true);
    }

    //---------------------------------------------------------------------

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    //---------------------------------------------------------------------
}
