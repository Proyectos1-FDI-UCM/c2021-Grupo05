using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricityLogic : MonoBehaviour
{
    [SerializeField]
    List<EnemyAI> enemies;

    [SerializeField]
    float visionConeReduceDistance;

    [SerializeField]
    float visionConeReduceAngle;

    [SerializeField]
    float velocityReduce;

    [SerializeField]
    GameObject darkMask;

    public void OffElectricty()
    {
        ReduceEnemyPower();
        MaskActivate();
    }
    void ReduceEnemyPower()
    {
        foreach (EnemyAI enemy in enemies)
            enemy.ChangeStats(visionConeReduceDistance, visionConeReduceAngle, velocityReduce);
    }

    void MaskActivate()
    {
        darkMask.SetActive(true);
    }


}
