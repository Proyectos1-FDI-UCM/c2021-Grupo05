using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricityLogic : MonoBehaviour
{
    [SerializeField]
    EnemyAI[] allEnemies;

    [SerializeField]
    float durationInSec;

    [SerializeField]
    float visionConeReduceDistance;

    [SerializeField]
    float visionConeReduceAngle;

    [SerializeField]
    float velocityReduce;

    [SerializeField]
    GameObject darkMask;

    private void Awake()
    {
        allEnemies = FindObjectsOfType<EnemyAI>();
    }

    public void OffElectricty()
    {
        ReduceEnemyPower();
        MaskActivate();
    }
    void ReduceEnemyPower()
    {
        for (int i = 0; i < allEnemies.Length; i++)
            if (allEnemies[i] != null)
                allEnemies[i].ChangeStats(visionConeReduceDistance, visionConeReduceAngle, velocityReduce);

        Invoke("RestoreEnemyPower", durationInSec);
    }

    void RestoreEnemyPower()
    {
        for (int i = 0; i < allEnemies.Length; i++)
            if (allEnemies[i] != null)
                allEnemies[i].ChangeStats(allEnemies[i].viewDistance, allEnemies[i].fov, allEnemies[i].passiveSpeed);
    }

    void MaskActivate()
    {
        darkMask.SetActive(true);
    }


}
