using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Raúl Saavedra de la Riera
public class BulletPrefabScript : MonoBehaviour
{
    private float bulletSpeed = 70f;

    private void Start()
    {
        GetComponent<Rigidbody2D>().velocity = transform.right * bulletSpeed;
        Destroy(gameObject, 3);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyAI enemyScript = collision.GetComponent<EnemyAI>();

        if (enemyScript != null) // Si se ha golpeado a un enemigo
            enemyScript.Damage(true);

        Destroy(gameObject);
    }
}
