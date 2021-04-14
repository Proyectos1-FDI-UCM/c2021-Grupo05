using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SpyMaster{
public class Soldier : MonoBehaviour, IDamageable<float>
{
    //Damageable Interface Method
    public void Damage(float damageTaken)
    {
        Debug.Log("DAMAGED");

        GetComponent<Animation>().Play("EnemDeath");
        GetComponent<Collider2D>().enabled = false;
        Destroy(this.gameObject, 1.0f);
    }
}
}
