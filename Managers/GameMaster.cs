using UnityEngine;
using UnityEngine.InputSystem;


public class GameMaster : MonoBehaviour
{   public GameObject upgradeMenu;
    public static GameObject currentRoom, previousRoom;
    public static GameMaster gm;
    public static bool unlockedDoubleJump;
    public GameObject upgradeNotif;
    public GameObject player;
    public static int gameMode;
    public GameObject campaignPrefab, endlessArenaPrefab, ringoutPrefab, mainMenu;
    private GameObject campaign, endlessArena, ringout;
    CameraFollow cameraFollow;
    Keyboard kb;
   
    public static void freezeTime()
    {
        if (Time.timeScale > 0)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    private void Update()
    {
        if(kb.f1Key.wasPressedThisFrame)
        {
            setGameMode(3);
        }
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cameraFollow = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();

        kb = Keyboard.current;

        setGameMode(0);

    }
  
    //-----------------------------------------------------------------------------------------------------------

    public static void roomSwitch(GameObject nextRoom)
    {
        previousRoom = currentRoom; //camera anterioara va deveni camera pe care playerul o paraseste
        nextRoom.SetActive(true); //activez parametrul primit
        previousRoom.SetActive(false); //dezactivez camera anterioara
        currentRoom = nextRoom; //am trecut la urmatoarea camera!

    }

   
    //-----------------------------------------------------------------------------------------------------------
    
    public void setGameMode(int mode)
    {
        gameMode = mode;

        switch (mode)
        {

            //--------------------------MAIN MENU-----------------------------
            case 0:
                {
                    mainMenu.active = true;

                    if (campaign != null)
                        Destroy(campaign);
                    else if (endlessArena != null)
                        Destroy(endlessArena);
                    else if (ringout != null)
                        Destroy(ringout);

                    PlayerHealth.currentHealth = PlayerHealth.maxHealth;
                    cameraFollow.enabled = false;
                    cameraFollow.GetComponent<CameraMultiFollow>().enabled = false;
                    break;
                }

            //--------------------------CAMPAIGN-----------------------------
            case 1:
                {

                    campaign = Instantiate(campaignPrefab, Vector3.zero, transform.rotation);
                    campaign.name = "Campaign";

                    if (endlessArena != null)
                    {
                        Destroy(endlessArena);
                    }

                    cameraFollow.enabled = true;
                    cameraFollow.GetComponent<CameraMultiFollow>().enabled = false;

                    Transform[] objs = campaign.GetComponentsInChildren<Transform>(true);

                    foreach(Transform obj in objs)
                    {
                        if (obj.name == "Upgrade Menu")
                            GetComponent<UpgradeManager>().UM = obj.gameObject;
                    }

                    break;
                }

            //--------------------------ENDLESS ARENA-----------------------------
            case 2:
                {
                    endlessArena = Instantiate(endlessArenaPrefab, Vector3.zero, transform.rotation);
                    endlessArena.name = "Endless Arena";
                    if (campaign != null)
                        Destroy(campaign);
                    else if (ringout != null)
                        Destroy(ringout);

                    Transform[] objs = endlessArena.GetComponentsInChildren<Transform>(true);

                    foreach (Transform obj in objs)
                    {
                        if (obj.name == "Shop")
                            GetComponent<UpgradeManager>().UM = obj.gameObject;
                    }

                    cameraFollow.enabled = true;
                    cameraFollow.GetComponent<CameraMultiFollow>().enabled = false;

                    break;
                }

            //--------------------------RINGOUT-----------------------------
            case 3:
                {
                    if (campaign != null)
                        Destroy(campaign);
                    else if (endlessArena != null)
                        Destroy(endlessArena);

                    ringout = Instantiate(ringoutPrefab, Vector3.zero, transform.rotation);
                    ringout.name = "Ringout";

                    var multiCam = cameraFollow.GetComponent<CameraMultiFollow>();
                    multiCam.enabled = true;
                    GameObject[] objects = GameObject.FindGameObjectsWithTag("Player");

                    for(int i=0; i<objects.Length; i++)

                    {
                        multiCam.players[i] = objects[i].GetComponent<Transform>();
                    }
                    
                    break;
                }

            default:
                {
                    Debug.LogError(gameMode + " is not a valid GameMode");
                    break;
                }
        }
       
    }
    void toggleMainMenu()
    {
        mainMenu.active = !mainMenu.active;
    }

}
