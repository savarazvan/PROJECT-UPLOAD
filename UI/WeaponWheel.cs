using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponWheel : MonoBehaviour
{
    public PlayerInventory inventory;
    public RectTransform wheelCursor;
    public InputAction navigateWheel;
    int dir, previousDir;
    Vector3 playerPos;

    //--------------------------------------------------------------

    private void Awake()
    {
        navigateWheel.performed += ctx => navigate(ctx);
    }

    Vector2[] positions = {new Vector2(0,33.7f),
                           new Vector2(33.7f, 0),
                           new Vector2(0, -33.7f),
                           new Vector2(-33.7f, 0)};

    float[] rotations = { 0, -90, 180, 90 };

    //--------------------------------------------------------------

    public void updateInventory(bool[] unlocked)
    {
        int i = 0;
       foreach(Transform child in transform)
        {
            if (child.gameObject.name == "WheelCursor")
                return;
            if (unlocked[i] && !child.gameObject.activeSelf)
                child.gameObject.SetActive(true);
            else if (!unlocked[i])
                child.gameObject.SetActive(false);
            i++;
        }
    }

    //--------------------------------------------------------------

    void navigate(InputAction.CallbackContext context)
    {
        var axis = context.ReadValue<Vector2>();
        if (axis.y > 0.5 && axis.x < 0.5 && axis.x > -0.5)
            dir = 0;
        else if (axis.y < 0.5 && axis.x < 0.5 && axis.x > -0.5)
            dir = 2;

        else if (axis.x > 0.5 && axis.y < 0.5 && axis.y > -0.5)
            dir = 1;
        else if (axis.x < 0.5 && axis.y < 0.5 && axis.y > -0.5)
            dir = 3;
    }

    //--------------------------------------------------------------

    private void LateUpdate()
    {
        playerPos = Camera.main.WorldToScreenPoint(inventory.GetComponent<Transform>().position);
        playerPos.y = Screen.height - playerPos.y;

     

        transform.position = playerPos;

        if (dir != previousDir)
        {
            wheelCursor.localPosition = positions[dir];
            Debug.Log(wheelCursor.rotation);
            wheelCursor.rotation = Quaternion.Euler(0, 0, rotations[dir]);
            previousDir = dir;   
        }
    }

    //--------------------------------------------------------------

    private void OnEnable()
    {
        navigateWheel.Enable();
    }

    private void OnDisable()
    {
        inventory.switchWeapon(dir);
        navigateWheel.Disable();
    }
}
