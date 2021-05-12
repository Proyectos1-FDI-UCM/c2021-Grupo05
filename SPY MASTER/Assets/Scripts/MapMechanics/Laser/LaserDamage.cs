using UnityEngine;
//Raúl Saavedra de la Riera
public class LaserDamage : MonoBehaviour
{
    [SerializeField]
    int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {        
        SimpleMov player = collision.gameObject.GetComponent<SimpleMov>();
        if(player != null)
        {
            player.TakeDamage();           
        }
    }
}
