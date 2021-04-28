using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraRotation : MonoBehaviour
{
    [Header("Si la cámara rota marca esta casilla")]
    [SerializeField]
    bool isRotating = false;

    [Header("La cámara girará 360 grados en el sentido horario")]
    [SerializeField]
    bool rotacionAgujas = false;

    [Header("La cámara girará 360 grados en el sentido antihorario")]
    [SerializeField]
    bool rotacionContraAgujas = false;

    [Header("La camara rotara del menor grado al mayor")]
    [SerializeField]
    float rotationRangeA = 45.0f;
    [SerializeField]
    float rotationRangeB = 15.0f;

    [Header("Velocidad de rotación")]
    [SerializeField]
    public float rotationVelocity = 25f;

    bool volviendo = false;

    CameraDetector cameraDetector;

    // Start is called before the first frame update
    void Start()
    {
        cameraDetector = GetComponent<CameraDetector>();

        float menorRotacion;

        if (rotationRangeA < 0)
            rotationRangeA = 360 + rotationRangeA;
        else if (rotationRangeA == 0)
            rotationRangeA = 1;


        if (rotationRangeB < 0)
            rotationRangeB = 360 + rotationRangeB;
        else if (rotationRangeB == 0)
            rotationRangeB = 1;


        if (rotationRangeB > rotationRangeA)
            menorRotacion = rotationRangeA;
        else
            menorRotacion = rotationRangeB;

        if (isRotating)
            transform.Rotate(0f, 0f, menorRotacion);
    }

    // Update is called once per frame
    void Update()
    {
        if (isRotating)
        {
            float rotationAmountA = rotationRangeA;
            float rotationAmountB = rotationRangeB;

            if (rotationAmountB > rotationAmountA)
            {
                if (!volviendo)
                {
                    if (transform.localRotation.eulerAngles.z > rotationAmountB)
                        volviendo = true;

                    transform.Rotate(0f, 0f, rotationVelocity * Time.deltaTime);
                }
                else if (volviendo)
                {
                    transform.Rotate(0f, 0f, -rotationVelocity * Time.deltaTime);

                    if (transform.localRotation.eulerAngles.z < rotationAmountA)
                        volviendo = false;
                }
            }
            else if (!volviendo)
            {
                if (transform.localRotation.eulerAngles.z > rotationAmountA)
                    volviendo = true;

                transform.Rotate(0f, 0f, rotationVelocity * Time.deltaTime);
            }
            else if (volviendo)
            {
                transform.Rotate(0f, 0f, -rotationVelocity * Time.deltaTime);

                if (transform.localRotation.eulerAngles.z < rotationAmountB)
                    volviendo = false;
            }
        }

        if (!isRotating && rotacionAgujas && !rotacionContraAgujas)
            transform.Rotate(0f, 0f, -rotationVelocity * Time.deltaTime);

        if (!isRotating && !rotacionAgujas && rotacionContraAgujas)
            transform.Rotate(0f, 0f, rotationVelocity * Time.deltaTime);    


        if (cameraDetector.followPlayer)transform.rotation = Quaternion.Euler(0, 0, GetAngleFromVector(cameraDetector.player.transform.position - transform.position));
    }

    float GetAngleFromVector(Vector2 dir)
    {
        dir = dir.normalized;

        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        return n;
    }
}
