using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//using namespace SpyMaster
//{
public class Bullet : MonoBehaviour
{
    ///<summary>
    ///Colisiona solo con los enemigos que implementan interfaz.
    ///Cambiarlo en un futuro(jugador tambien implementa damageable/ chocar con muros/etc.)
    ///<summary>

    //private float bulletSpeed = 60f;

    //private void Start()
    //{
    //    GetComponent<Rigidbody2D>().velocity = transform.right * bulletSpeed;
    //}

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    EnemyAI enemyScript = collision.GetComponent<EnemyAI>();

    //    if (enemyScript != null) // Si se ha golpeado a un enemigo
    //    {
    //        Debug.Log("ENEMY DAMAGED");
    //        enemyScript.Damage();
    //    }
    //}

    //private void OnTriggerEnter2D(Collider2D col)
    //{
    //    Debug.Log("BULLET ONTRIGGER ENTER");

    //    IDamageable<float> damageable = col.GetComponent<IDamageable<float>>();
    //    if (damageable == null)
    //    {
    //        return;
    //    }
    //    Debug.Log("HIT ENEMY");

    //    //Argumento Generic <T> (provisional)
    //    damageable.Damage(1.0f);
    //    Destroy(this.gameObject);
    //}


    //private void OnTriggerEnter2D(Collider2D col)
    //{
    //    Debug.Log("BULLET ONTRIGGER ENTER");

    //    if (col.GetComponent<Soldier>() != null)
    //    {
    //        Debug.Log("HIT ENEMY");

    //        //Argumento Generic <T> (provisional)
    //        col.GetComponent<Soldier>().Damage(1.0f);
    //        Destroy(this.gameObject);
    //    }
    //}

}

//}
