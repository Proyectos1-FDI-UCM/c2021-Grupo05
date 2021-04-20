using UnityEngine;

public class LaserDamage : MonoBehaviour
{
    [SerializeField]
    int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {        
        PlayerManager player = collision.gameObject.GetComponent<PlayerManager>();
        if(player != null)
        {
            player.Damage(damage);
            Debug.Log("Player_IsDamage");
        }
    }
}
