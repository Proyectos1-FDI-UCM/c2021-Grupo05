using UnityEngine;

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
