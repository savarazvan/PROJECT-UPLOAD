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
            currentMenu.positions[pointerPos], 20f * Time.deltaTime);

        if (rectTransform.sizeDelta != currentMenu.rectScale)
            rectTransform.sizeDelta = Vector3.Lerp(rectTransform.sizeDelta,
                currentMenu.rectScale, 20f * Time.deltaTime);


    }

    //---------------------------------------------------------------------

    void navigate(Vector2 dir)
    {
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
            else if (dir.x <0)
                pointerPos--;
        }
    }

    //---------------------------------------------------------------------

    void Select(int ID, int pos, bool goBack)
    {
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
                                    changeMenu(MenuList.campaignSelect);
                                    break;
                                }
                            case 1:
                                {
                                    gameMaster.setGameMode(2);
                                    GetComponentsInParent<Transform>()[1].gameObject.SetActive(false);
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
                        changeMenu(MenuList.mainMenu);
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
        }
    }

    //---------------------------------------------------------------------

    void changeMenu(MenuStruct.Menu menu)
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
