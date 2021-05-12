using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Raúl Saavedra de la Riera
//Ignacio del Castisllo
public class ElectricityLogic : MonoBehaviour
{
    [SerializeField]
    EnemyAI[] allEnemies;

    CameraDetector[] allCameras; 

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
        allCameras = FindObjectsOfType<CameraDetector>();
    }

    public void OffElectricty()
    {
        ReduceEnemyPower();
        MaskActivate();
    }
    void ReduceEnemyPower()
    {
        // Cambiar Stats de los enemigos
        for (int i = 0; i < allEnemies.Length; i++)
            if (allEnemies[i] != null)
                allEnemies[i].ChangeStats(visionConeReduceDistance, visionConeReduceAngle, velocityReduce);

        // Apagar camaras
        for (int i = 0; i < allCameras.Length; i++)
            if (allCameras[i] != null)
            {
                allCameras[i].viewDistanceVar = 0;
                allCameras[i].visionConeScript.SetViewDistance(0);
            }


        Invoke("RestoreEnemyPower", durationInSec);
    }

    void RestoreEnemyPower()
    {
        // Cambiar Stats de los enemigos
        for (int i = 0; i < allEnemies.Length; i++)
            if (allEnemies[i] != null)
                allEnemies[i].ChangeStats(allEnemies[i].viewDistance, allEnemies[i].fov, allEnemies[i].passiveSpeed);

        // Encender camaras
        for (int i = 0; i < allCameras.Length; i++)
            if (allCameras[i] != null)
            {
                allCameras[i].viewDistanceVar = allCameras[i].viewDistance;
                allCameras[i].visionConeScript.SetViewDistance(allCameras[i].viewDistance);
            }


        darkMask.SetActive(false);
    }

    void MaskActivate()
    {
        darkMask.SetActive(true);
    }
}
