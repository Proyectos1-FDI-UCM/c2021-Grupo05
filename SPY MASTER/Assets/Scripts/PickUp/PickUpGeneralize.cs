using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Raul Saavedra de la Riera
public class PickUpGeneralize : MonoBehaviour
{
    [SerializeField]
    bool health;
    [SerializeField]
    bool ammo;

    [SerializeField]
    GameObject life, bullet;
  
    // Start is called before the first frame update
    void Start()
    {
        if(health)
        {
            life.SetActive(true); 
        }
        if(ammo)
        {
            bullet.SetActive(true);
        }
    }


    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collision");
        SimpleMov player = collision.gameObject.GetComponentInParent<SimpleMov>();
        if (player != null)
        {
            Debug.Log("col with player");
            if (health)
            {
                if (player.CheckMaxLive())
                {
                    player.GetLive();
                    Destroy(this.gameObject);
                }

            }

            if (ammo)
            {
                if(player.CheckBullets())
                {
                    Debug.Log("ammo");

                    player.GetAmmo();
                    Destroy(this.gameObject);
                }
               
            }



        }
    }

}
