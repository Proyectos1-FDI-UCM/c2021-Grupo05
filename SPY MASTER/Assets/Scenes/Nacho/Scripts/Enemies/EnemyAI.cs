using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


public class EnemyAI : MonoBehaviour
{
    // ADJUSTABLE VARIABLES
    [SerializeField]
    [Header("FieldOfView")]
    float fov;
    [SerializeField]
    [Header("Distancia a la que puede ver el enemigo")]
    float viewDistance;

    [SerializeField]
    [Header("Tiempo que esta el enemigo persiguiendo al jugador antes de rendinrse y volver a su patrulla")]
    float timeFollowingPlayer; // Tiempo que esta el enemigo persiguiendo al jugador antes de rendinrse y volver a su patrulla
    float followPlayerTimer;

    [SerializeField]
    [Header("Tiempo entre balas que dispara el enemigo")]
    float cadence; // Tiempo entre balas que dispara el enemigo
    float cadenceTimer;

    [SerializeField]
    [Header("Almacena todas las posiciones de patrulla")]
    Transform[] patrolPoints; // Almacena todas las posiciones de patrulla
    int patrolIndex; // Define a que posicion se debe ir en este momento


    bool followPlayer; // Valdra true cuando se detecte al jugador, y volvera a false despues de X segundos persiguiendo al jugador
    GameObject player;
    Rigidbody2D rb;

    // PATH FINDING
    [Header("PATHFINDING")]
    public Transform target;

    [SerializeField]
    float passiveSpeed;
    [SerializeField]
    float followSpeed;
    [SerializeField]
    float alertedSpeed;
    float speed;
    [SerializeField]
    float nextWayPointDistance = 3f; // Distancia a la que se tiene que acercar el objeto para dejar de perseguirlo

    Path path;
    Seeker seeker;
    int currentWayPoint = 0;
    bool reachedEndOfPath = false;

    // SET UP
    [Header("SET UP")]

    [SerializeField]
    GameObject visionConePrfb;
    VisionCone visionConeScript;

    [SerializeField]
    GameObject visionConeGroup;

    [SerializeField]
    Transform gfx;

    [SerializeField]
    Animator animator;

    [SerializeField]
    GameObject bulletPrfb;

    [SerializeField]
    Transform gun;
    [SerializeField]
    Transform shootPoint;

    private void Awake()
    {
        player = FindObjectOfType<BlinkScript>().gameObject;
        followPlayer = false; // Empezar en modo "Passive"
        speed = passiveSpeed; // Empezar a la velocidad de patrulla
        cadenceTimer = cadence;
        gun.rotation = Quaternion.identity;

        // PATHFINDING
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        // CONO VISION
        visionConeScript = Instantiate(visionConePrfb, visionConeGroup.transform).GetComponent<VisionCone>();
        visionConeScript.SetFov(fov);
        visionConeScript.SetViewDistance(viewDistance);
    }

    private void Update()
    {
        if (gun != null) // Si el enemigo esta muerto, no moverse (Se estara viendo la animacion de muerte de enemigo)
        {
            CheckPlayer(); // Comprobar si el jugador es visible, cambia el estado si se detecta al jugador

            if (!followPlayer) Patrol(); // Si no 

            // Actualiza los valores del Cono de vision para que este se dibuje correctamente
            visionConeScript.SetOrigin(transform.position);
            if (rb.velocity != Vector2.zero)
                visionConeScript.SetDirection(rb.velocity);
        }
    }

    void Patrol()
    {
        if (Vector2.Distance(transform.position, patrolPoints[patrolIndex].position) < 1) // Si se ha acercado lo suficiente al punto de patrulla, cambiar al siguiente
        {
            if (patrolIndex + 1 == patrolPoints.Length) patrolIndex = 0;
            else patrolIndex++;

            seeker.StartPath(rb.position, patrolPoints[patrolIndex].position, OnPathComplete);
        }
    }

    private void FixedUpdate()
    {
        PathFinding();
    }

    void PathFinding()
    {
        if (path == null) return;

        if (currentWayPoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWayPoint] - rb.position).normalized; // Direccion en la que se encuentra el siguiente pto del path
        Vector2 force = direction * speed * Time.deltaTime;

        //rb.velocity = force;
        rb.AddForce(force); // Mover 

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWayPoint]);

        if (distance < nextWayPointDistance)
            currentWayPoint++;

        UpdateOrientation();
    }

    void UpdateOrientation()
    {
        // Rotar la parte visual del enemigo
        if (rb.velocity.x >= 0.01f)
        {
            gfx.localScale = new Vector3(Mathf.Abs(gfx.localScale.x), gfx.localScale.y, 1); // Sprite del enemigo

            if (!followPlayer) gun.localScale = new Vector3(Mathf.Abs(gun.localScale.x), 1, 1); // Sprite de la pistola
        }
        else if (rb.velocity.x <= -0.01f)
        {
            gfx.localScale = new Vector3(-Mathf.Abs(gfx.localScale.x), gfx.localScale.y, 1); // Sprite del enemigo

            if (!followPlayer) gun.localScale = new Vector3(-Mathf.Abs(gun.localScale.x), 1, 1); // Sprite de la pistola
        }

        // Rotar pistola
        if (followPlayer)
        {
            if (transform.position.x < player.transform.position.x) // Cambiar sprite de la pistola dependiendo de la posicion del jugador
                gun.localScale = new Vector3(1, Mathf.Abs(gun.localScale.y), 1); // Sprite de la pistola
            else
                gun.localScale = new Vector3(1, -Mathf.Abs(gun.localScale.y), 1); // Sprite de la pistola
        }
    }

    void UpdatePathToPlayer()
    {
        // Crear el path, donde hay que aclarar desde que posicion, hacia que posicion, y la funcion que queremos que invoque al terminar de calcularla
        if (seeker.IsDone()) // Si no se esta calculando un path actualmente
            seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    void OnPathComplete(Path p) // Se llama este metodo cada vez que se termine de calcular un Path usando "seeker.StartPath", siendo "p" el Path creado
    {
        if (!p.error) // Si en la creacion del Path no ha habido ningun error
        {
            path = p; // Cambiar el path actual por uno actualizado
            currentWayPoint = 0; // Cambiar el punto del path por el que se iba por el primero
        }
    }

    void CheckPlayer() // Comprueba si el jugador esta visible para este enemigo, y se encarga de perseguirlo en tal caso o de volver a la ruta prevista
    {
        // Decrementar cadencia
        if (followPlayer) // Hacer daño al jugador cada cierto tiempo si este esta en el campo de vision
        {
            if (cadenceTimer - Time.deltaTime < 0) cadenceTimer = 0; // Decrementar contador
            else cadenceTimer -= Time.deltaTime;
        }

        // Decrementar tiempo persiguiendo al jugador
        if (followPlayerTimer - Time.deltaTime > 0)
        {
            UpdateGunRotation();
            followPlayerTimer -= Time.deltaTime; // Decrementar tiempo
        }
        else followPlayerTimer = 0;

        // Si el tiempo de perseguir se acaba
        if (followPlayer && followPlayerTimer == 0) // Dejar de seguir al jugador
        {
            CancelInvoke(); // Dejar de llamar a la funcion "UpdatePathToPlayer"
            followPlayer = false;
            speed = alertedSpeed;
            cadenceTimer = cadence; // Resetear cadencia
            animator.SetTrigger("Idle");
            path = null; // Poner el path que se estaba siguiendo actualmente a null para que no persiga al jugador durante el "StunnedTime"
            Invoke("StunnedTime", 5); // Tiempo en el que el enemigo se queda parado sin hacer nada durante 2 segundos tras darse por vencido persiguiendo al jugador
        }

        if (Vector2.Distance(transform.position, player.transform.position) <= viewDistance) // Si el jugador esta cerca
        {
            Vector3 playerDir = (player.transform.position - transform.position).normalized;

            if (Vector3.Angle(rb.velocity, playerDir) < fov / 2)
            {

                string[] collideWithThisLayers = new string[2] { "Player", "Wall" };
                LayerMask collideWithThisMasks = LayerMask.GetMask(collideWithThisLayers);
                RaycastHit2D ray = Physics2D.Raycast(transform.position, playerDir, viewDistance, collideWithThisMasks); // Lanzar un raycast hacia el jugador


                if (ray.collider.gameObject.layer == 8) // Si el ray cast alcanza al jugador
                {
                    if (cadenceTimer <= 0)
                    {
                        cadenceTimer = cadence;
                        Debug.Log("PLAYER.TAKEDAMAGE()");
                        Shoot();
                    }
                    
                    if (!followPlayer) // Si todavia no se habia descubierto
                    {
                        followPlayer = true;
                        Debug.Log("PLAYER.TAKEDAMAGE()");
                        UpdateGunRotation(); // Actualizar la rotacion de la pistola antes de disparar con ella
                        UpdateOrientation();
                        Shoot();
                        CancelInvoke(); // Cancelar otros invokes (En el caso en el que ya se estuviese persiguiendo al jugador y vuelve a ser detectado (reiniciar cuenta atras))
                        speed = followSpeed;
                        cadenceTimer = cadence;
                        animator.SetTrigger("Run"); // Actualizar animacion
                        gfx.gameObject.GetComponent<SpriteRenderer>().color = Color.red; // Cambiar color del sprite
                        gun.GetComponentInChildren<SpriteRenderer>().color = Color.red; // Cambiar color del sprite de la pistola
                        InvokeRepeating("UpdatePathToPlayer", 0, 0.2f); // Actualizar el Path hacia el player cada X segundos
                    }
                    followPlayerTimer = timeFollowingPlayer;
                }
            }
        }
    }

    void UpdateGunRotation() // Rota la pistola para que apunte al jugador
    {
        Vector2 diff = player.transform.position - transform.position;
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg; // Calcular la rotacion para apuntar al jugador
        gun.rotation = Quaternion.Euler(0, 0, rot_z);
    }

    void Shoot()
    {
        Vector2 diff = player.transform.position - transform.position;
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg; // Calcular la rotacion para apuntar al jugador
        Rigidbody2D bulletInstance = Instantiate(bulletPrfb, shootPoint.position, Quaternion.Euler(0, 0, rot_z)).GetComponent<Rigidbody2D>();
        Destroy(bulletInstance, 5f);//seguro de destruccion

        player.GetComponent<SimpleMov>().TakeDamage();
    }
    void StunnedTime()
    {
        animator.SetTrigger("Walk");
        gun.rotation = Quaternion.identity; // Poner pistola en posicion relajada
        gfx.gameObject.GetComponent<SpriteRenderer>().color = Color.yellow; // Cambiar color del sprite
        gun.GetComponentInChildren<SpriteRenderer>().color = Color.yellow; // Cambiar color del sprite de la pistola
        seeker.StartPath(rb.position, patrolPoints[patrolIndex].position, OnPathComplete); // Volver al punto en el que dejo la patrulla
    }

    public bool IsFollowPlayer()
    {
        return followPlayer;
    }

    public void ChangeStats(float visionRange, float visionAngle, float newSpeed)
    {
        fov = visionAngle;
        viewDistance = visionRange;
        passiveSpeed = newSpeed;
    }

    public void Damage()
    {
        path = null;
        animator.SetTrigger("EnemyDeath");
        Destroy(visionConeScript.gameObject);
        Destroy(gun.gameObject);
        GetComponent<Collider2D>().enabled = false;
        Destroy(this.gameObject, 1.0f);
    }
}
