using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.IO;

public class CampaignSelect : MonoBehaviour
{
    public PlayerInput controls;

    public Image slotCursor, slotOption;
    public int slotCursorPos, slotOptionPos;
    float[] slotCursorTransform = { -450, 0, 450 }, slotOptionTransform = { -118, -164 };
    RectTransform cursor, option;
    Vector2 tempPosCursor, tempPosOption;
    GameMaster gameMaster;
    public GameObject mainMenu;

    private void Awake()
    {
        controls = new PlayerInput();
        controls.Menu.Navigate.performed += ctx => navigate(ctx.ReadValue<Vector2>());
        controls.Menu.Select.performed += ctx => selectSlot(slotCursorPos);

        cursor = slotCursor.GetComponent<RectTransform>();
        option = slotOption.GetComponent<RectTransform>();

        gameMaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }



    void Update()
    {

        if (slotCursorPos >= slotCursorTransform.Length)
            slotCursorPos = 0;
        else if (slotCursorPos < 0)
            slotCursorPos = slotCursorTransform.Length - 1;

        if (slotOptionPos >= slotOptionTransform.Length)
            slotOptionPos = 0;
        else if (slotOptionPos < 0)
            slotOptionPos = slotOptionTransform.Length - 1;

        //---------------------------------------------------------------------

        tempPosCursor = new Vector2(slotCursorTransform[slotCursorPos], cursor.localPosition.y);
        tempPosOption = new Vector2(option.localPosition.x, slotOptionTransform[slotOptionPos]);

        cursor.localPosition = Vector2.Lerp(cursor.localPosition, tempPosCursor, 20f * Time.deltaTime);
        option.localPosition = Vector2.Lerp(option.localPosition, tempPosOption, 20f * Time.deltaTime);

    }

    //--------------------------------------------

    void navigate(Vector2 dir)

    {
        Debug.Log(dir);
        if (dir.x > 0)
            slotCursorPos++;
        else if (dir.x < 0)
            slotCursorPos--;

        if (dir.y < 0)
            slotOptionPos++;
        else if (dir.y > 0)
            slotOptionPos--;

    }

    //--------------------------------------------

    void selectSlot(int slot)
    {
        if (File.Exists(Application.persistentDataPath + "/saveslot" + (slot + 1) + ".data"))
        {
            gameMaster.setGameMode(1);
            LoadManager.Load(slot + 1);
            mainMenu.SetActive(false);
        }
        else Debug.Log("slot " + (slot + 1) + " is empty!");
    }

    //--------------------------------------------

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
}
