using UnityEngine;

public class AutoSaveCheckpoint : MonoBehaviour
{
    public GameMaster gameMaster;

    //------------------------------------------------------

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            switch (LoadManager.currentSlot)
            {

                //------------------------------------------------------

                case 0:
                    {
                        GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>()
                            .pauseGame(collision.GetComponent<CharacterMovement>(), true);

                        Destroy(gameObject);
                        break;
                    }

                //------------------------------------------------------

                case 4:
                    {
                        Destroy(gameObject);
                        return;
                    }

                //------------------------------------------------------

                default:
                    {
                        SaveSystem.SavePlayer(collision.transform,
                            LoadManager.currentSlot,
                        collision.GetComponent<PowerupSlot>().powerupSlot);

                        Destroy(gameObject);

                        break;
                    }
            }
        }
    }

   
    
}
