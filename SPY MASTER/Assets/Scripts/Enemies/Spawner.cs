using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;

    public void SpawnEnemy()
    {
        Instantiate(enemyPrefab, Random.insideUnitCircle * 4.5f, Quaternion.identity);
    }
}
