﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDetector : MonoBehaviour
{
    [SerializeField]
    [Header("FieldOfView")]
    float fov;

    [SerializeField]
    [Header("Distancia a la que puede ver el enemigo")]
    float viewDistance;

    [SerializeField]
    GameObject player;

    [SerializeField]
    GameObject visionConePrfb;
    VisionCone visionConeScript;

    [SerializeField]
    GameObject visionConeGroup;


    // Start is called before the first frame update
    void Start()
    {
        visionConeScript = Instantiate(visionConePrfb, visionConeGroup.transform).GetComponent<VisionCone>();
        visionConeScript.SetFov(fov);
        visionConeScript.SetViewDistance(viewDistance);
    }

    // Update is called once per frame
    void Update()
    {

        visionConeScript.SetOrigin(transform.position);
        visionConeScript.SetDirection(AngleToVector(transform.localRotation.eulerAngles.z));
        CheckPlayer();
    }

    Vector3 AngleToVector(float angle) // Devuelve el Vector3 que corresponde al angulo con una magnitud de 1.
    {
        // angle = 0 - 360
        float angleRad = angle * (Mathf.PI / 180f); // Pasar el angulo de grados a radianes
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad)); // Utilizar "Mathf" para saber el coseno y el seno y almacenarlo en el vector
    }

    void CheckPlayer() // Comprueba si el jugador esta visible para este enemigo, y se encarga de perseguirlo en tal caso o de volver a la ruta prevista
    {
        if (Vector2.Distance(transform.position, player.transform.position) <= viewDistance) // Si el jugador esta cerca
        {
            Vector3 playerDir = (player.transform.position - transform.position).normalized;

            if (Vector3.Angle(AngleToVector(transform.localRotation.eulerAngles.z), playerDir) < fov / 2)
            {

                string[] collideWithThisLayers = new string[2] { "Player", "Wall" };
                LayerMask collideWithThisMasks = LayerMask.GetMask(collideWithThisLayers);
                RaycastHit2D ray = Physics2D.Raycast(transform.position, playerDir, viewDistance, collideWithThisMasks); // Lanzar un raycast hacia el jugador


                if (ray.collider.gameObject.layer == 8) // Si el ray cast alcanza al jugador
                {
                    Debug.Log("detected");
                }
            }
        }
    }
}