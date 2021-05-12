using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Raúl Saavedra de la Riera
public class PlayerManager : MonoBehaviour
{
    public int maxLife;
        int live;

    bool isDead;
    void Start()
    {
        live = maxLife;
    }

    // Update is called once per frame
    public void Damage(int damage)
    {
        live -= damage;

        if(live < 1)
            isDead = true;
        
    }

    public void isAlive(bool live)
    {
        if (!isDead) live = true;
    }
}
