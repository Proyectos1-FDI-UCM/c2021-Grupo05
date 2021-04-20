using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiologistDetectRange : MonoBehaviour
{

    Biologist_Enemy biologistScript;

    private void Awake()
    {
        biologistScript = GetComponentInParent<Biologist_Enemy>();
        EnemyAI[] allEnemiesArray = FindObjectsOfType<EnemyAI>();

        // Añadir todos los enemigos que esten dentro del rango al principio de la partida
        for (int i = 0; i < allEnemiesArray.Length; i++)
        {
            if (Vector2.Distance(transform.position, allEnemiesArray[i].transform.position) < biologistScript.buffRange) // Si el enemigo esta lo suficientemente cerca, añadirlo a la lista
            {
                if (allEnemiesArray[i] != transform.parent) // No añadirse a si mismo
                {
                    biologistScript.enemiesInRange.Add(allEnemiesArray[i]);
                    biologistScript.enemiesInRange[i] = biologistScript.enemiesInRange[i];
                }
            }
        }

        Debug.Log("ENEMIES IN RANGE");

        for (int i = 0; i < biologistScript.enemiesInRange.Count; i++)
        {
            Debug.Log("biologistScript.enemiesInRange [" + i + "] = " + biologistScript.enemiesInRange[i]);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) // Buffear la velocidad a todos los enemigos que entren en el rango de Buff
    {
        EnemyAI enemyScript = collision.GetComponent<EnemyAI>();

        if (enemyScript != null) // Si ha entrado un enemigo
        {
            Debug.Log("BUFF RANGE COLLIDED WITH " + collision.name);

            BuffEnemy(enemyScript);
        }
    }
    private void OnTriggerExit2D(Collider2D collision) // Buffear la velocidad a todos los enemigos que entren en el rango de Buff
    {
        EnemyAI enemyScript = collision.GetComponent<EnemyAI>();

        if (enemyScript != null) // Si ha entrado un enemigo
        {
            Debug.Log("BUFF RANGE EXIT COLLIDER WITH " + collision.name);

            NerfEnemy(enemyScript);
        }
    }

    void BuffEnemy(EnemyAI enemyScript)
    {
        enemyScript.speed *= 2;
        enemyScript.passiveSpeed *= 2;
        enemyScript.alertedSpeed *= 2;
        enemyScript.followSpeed *= 2;
    }

    void NerfEnemy(EnemyAI enemyScript)
    {
        enemyScript.speed /= 2;
        enemyScript.passiveSpeed /= 2;
        enemyScript.alertedSpeed /= 2;
        enemyScript.followSpeed /= 2;
    }

}
