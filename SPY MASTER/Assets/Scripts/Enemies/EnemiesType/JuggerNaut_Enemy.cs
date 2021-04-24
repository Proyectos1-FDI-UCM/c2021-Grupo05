using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JuggerNaut_Enemy : EnemyAI
{

    protected override void DamageByBullet()
    {
        // Efecto de blindado en vez de recibir daño por la bala
        Debug.Log("JuggerNaut Hitted by Bullet");
    }
}
