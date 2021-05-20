//Isidro Lucas
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDetector : MonoBehaviour
{
    [SerializeField]
    [Header("FieldOfView")]
    float fov;

    [Header("Distancia a la que puede ver el enemigo")]
    public float viewDistance;
    public float viewDistanceVar;

    public GameObject player;

    [SerializeField]
    GameObject visionConePrfb;
    public VisionCone visionConeScript;

    [SerializeField]
    GameObject visionConeGroup;

    public bool followPlayer;

    bool detected = false;
    float soundCD = 3f;

    // Start is called before the first frame update
    void Start()
    {
        viewDistanceVar = viewDistance;

        visionConeScript = Instantiate(visionConePrfb, visionConeGroup.transform).GetComponent<VisionCone>();
        visionConeScript.SetFov(fov);
        visionConeScript.SetViewDistance(viewDistanceVar);
    }

    // Update is called once per frame
    void Update()
    {

        visionConeScript.SetOrigin(transform.position);
        visionConeScript.SetDirection(AngleToVector(transform.localRotation.eulerAngles.z));
        if(player != null)
        CheckPlayer();

        if (detected)
        {
            soundCD -= Time.deltaTime;
            if (soundCD <= 0)
            {
                detected = false;
            }
        }
    }

    Vector3 AngleToVector(float angle) // Devuelve el Vector3 que corresponde al angulo con una magnitud de 1.
    {
        // angle = 0 - 360
        float angleRad = angle * (Mathf.PI / 180f); // Pasar el angulo de grados a radianes
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad)); // Utilizar "Mathf" para saber el coseno y el seno y almacenarlo en el vector
    }

    
    void CheckPlayer() // Comprueba si el jugador esta visible para este enemigo, y se encarga de perseguirlo en tal caso o de volver a la ruta prevista
    {
        if (Vector2.Distance(transform.position, player.transform.position) <= viewDistanceVar) // Si el jugador esta cerca
        {
            Vector3 playerDir = (player.transform.position - transform.position).normalized;

            if (Vector3.Angle(AngleToVector(transform.localRotation.eulerAngles.z), playerDir) < fov / 2)
            {
                string[] collideWithThisLayers = new string[2] { "Player", "Wall" };
                LayerMask collideWithThisMasks = LayerMask.GetMask(collideWithThisLayers);
                RaycastHit2D ray = Physics2D.Raycast(transform.position, playerDir, viewDistanceVar, collideWithThisMasks); // Lanzar un raycast hacia el jugador

                if (ray.collider.gameObject.layer == 8) // Si el ray cast alcanza al jugador
                {
                    if (!enemiesAlerted) MakeEnemiesAlerted();

                    followPlayer = true;
                    CancelInvoke();
                    Invoke("StopFollowPlayer", 2);

                    DetectedSound();
                }
            }
        }
    }

    void StopFollowPlayer()
    {
        followPlayer = false;
    }

    bool enemiesAlerted;
    int alertRange = 10;
    void MakeEnemiesAlerted()
    {
        EnemyAI[] allEnemies = FindObjectsOfType<EnemyAI>();

        for (int i = 0; i < allEnemies.Length; i++)
        {
            EnemyAI thisEnemy = allEnemies[i];
            if (Vector2.Distance(transform.position, thisEnemy.transform.position) < alertRange)
            {
                thisEnemy.speed = thisEnemy.alertedSpeed;

                thisEnemy.gfx.gameObject.GetComponent<SpriteRenderer>().color = Color.yellow; // Cambiar color del sprite
                thisEnemy.gun.GetComponentInChildren<SpriteRenderer>().color = Color.yellow; // Cambiar color del sprite de la pistola
            }
        }
    }


    void DetectedSound()
    {
        if (!detected)
        {
            FindObjectOfType<AudioManager>().Play("Detected");
            detected = true;
            soundCD = 3f;
        }
    }
}
