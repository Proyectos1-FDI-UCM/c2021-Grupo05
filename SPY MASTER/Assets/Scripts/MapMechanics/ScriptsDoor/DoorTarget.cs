
using UnityEngine;
//Raúl Saavedra de la Riera
public class DoorTarget : MonoBehaviour
{
    [SerializeField]
    GameObject doorBlock;

    [SerializeField]
    [Header("Tipo de puerta")]
    bool redDoor;
    [SerializeField]
    bool blueDoor;
    [SerializeField]
    bool yellowDoor;

    void CheckOpen(PlayerTarget player)
    {
        if (redDoor && player.redTarget)
        {
            doorBlock.SetActive(false);
            player.redTarget = false;
            AudioManager.GetInstance().Play("Door");
        }
        else if (blueDoor && player.blueTarget)
        {
            doorBlock.SetActive(false);
            player.blueTarget = false;
            AudioManager.GetInstance().Play("Door");
        }
        else if (yellowDoor && player.yellowTarget)
        {
            doorBlock.SetActive(false);
            player.yellowTarget = false;
            AudioManager.GetInstance().Play("Door");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerTarget player = collision.gameObject.GetComponent<PlayerTarget>();
        if (player != null)
            CheckOpen(player);
    }
  
   
  
}
