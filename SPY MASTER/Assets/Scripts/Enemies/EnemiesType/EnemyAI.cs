// Nacho

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


public class EnemyAI : MonoBehaviour
{
    // ADJUSTABLE VARIABLES
    [SerializeField]
    [Header("FieldOfView")]
    public float fov;
    protected float fovVar;
    [SerializeField]
    [Header("Distancia a la que puede ver el enemigo")]
    public float viewDistance;
    protected float viewDistanceVar;

    [SerializeField]
    [Header("Tiempo que esta el enemigo persiguiendo al jugador antes de rendinrse y volver a su patrulla")]
    protected float timeFollowingPlayer; // Tiempo que esta el enemigo persiguiendo al jugador antes de rendinrse y volver a su patrulla
    protected float followPlayerTimer;

    [SerializeField]
    [Header("Tiempo entre balas que dispara el enemigo")]
    protected float cadence; // Tiempo entre balas que dispara el enemigo
    protected float cadenceTimer;

    [SerializeField]
    [Header("Almacena todas las posiciones de patrulla")]
    Transform[] patrolPoints; // Almacena todas las posiciones de patrulla
    int patrolIndex; // Define a que posicion se debe ir en este momento


    protected bool followPlayer; // Valdra true cuando se detecte al jugador, y volvera a false despues de X segundos persiguiendo al jugador
    protected GameObject player;
    protected Rigidbody2D rb;

    // PATH FINDING
    [Header("PATHFINDING")]
    public Transform target;

    // Velocidades publicas para que el biologo las pueda modificar facilmente
    [SerializeField]
    public float passiveSpeed;
    [SerializeField]
    public float followSpeed;
    [SerializeField]
    public float alertedSpeed;
    public float speed;
    [SerializeField]
    float nextWayPointDistance = 3f; // Distancia a la que se tiene que acercar el objeto para dejar de perseguirlo

    protected Path path;
    protected Seeker seeker;
    int currentWayPoint = 0;
    bool reachedEndOfPath = false;

    // SET UP
    [Header("SET UP")]

    [SerializeField]
    GameObject visionConePrfb;
    protected VisionCone visionConeScript;

    [SerializeField]
    GameObject visionConeGroup;

    public Transform gfx;

    protected Animator gfxAnimator;

    [SerializeField]
    protected GameObject bulletPrfb;

    public Transform gun;
    [SerializeField]
    protected Transform shootPoint;

    private void Awake()
    {
        player = FindObjectOfType<BlinkScript>().gameObject;
        followPlayer = false; // Empezar en modo "Passive"
        speed = passiveSpeed; // Empezar a la velocidad de patrulla
        cadenceTimer = cadence;
        gun.rotation = Quaternion.identity;
        gfxAnimator = GetComponentInChildren<Animator>();
        gfxAnimator.SetTrigger("Walk");


        // PATHFINDING
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        fovVar = fov;
        viewDistanceVar = viewDistance;

        // CONO VISION
        visionConeScript = Instantiate(visionConePrfb, visionConeGroup.transform).GetComponent<VisionCone>();
        visionConeScript.SetFov(fovVar);
        visionConeScript.SetViewDistance(viewDistanceVar);
    }

    private void Update()
    {
        if (gun != null) // Si el enemigo esta muerto, no moverse (Se estara viendo la animacion de muerte de enemigo)
        {
            UpdateFollowParameters();

            CheckPlayer(); // Comprobar si el jugador es visible, cambia el estado si se detecta al jugador

            if (!followPlayer) Patrol(); // Si no 

            UpdateVisionCone();

            ExtraStuff();

            //gfxAnimator.SetFloat("Speed", rb.velocity.magnitude);
        }
    }

    protected virtual void UpdateVisionCone()
    {
        // Actualiza los valores del Cono de vision para que este se dibuje correctamente
        visionConeScript.SetOrigin(transform.position);
        if (rb.velocity != Vector2.zero)
            visionConeScript.SetDirection(rb.velocity);
    }

    protected virtual void ExtraStuff() { }

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

    protected void UpdateOrientation()
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
        if (followPlayer && gun != null)
        {
            if (transform.position.x < player.transform.position.x) // Cambiar sprite de la pistola dependiendo de la posicion del jugador
                gun.localScale = new Vector3(1, Mathf.Abs(gun.localScale.y), 1); // Sprite de la pistola
            else
                gun.localScale = new Vector3(1, -Mathf.Abs(gun.localScale.y), 1); // Sprite de la pistola
        }
    }

    protected virtual void UpdatePathToPlayer() // "virtual" para que el enemigo Biologo pueda modificar este metodo y huir en vez de perseguir al jugador
    {
        // Crear el path, donde hay que aclarar desde que posicion, hacia que posicion, y la funcion que queremos que invoque al terminar de calcularla
        if (seeker.IsDone()) // Si no se esta calculando un path actualmente
            seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    protected void OnPathComplete(Path p) // Se llama este metodo cada vez que se termine de calcular un Path usando "seeker.StartPath", siendo "p" el Path creado
    {
        if (!p.error) // Si en la creacion del Path no ha habido ningun error
        {
            path = p; // Cambiar el path actual por uno actualizado
            currentWayPoint = 0; // Cambiar el punto del path por el que se iba por el primero
        }
    }


    protected virtual void CheckPlayer() // Comprueba si el jugador esta visible para este enemigo, y se encarga de perseguirlo en tal caso o de volver a la ruta prevista
    {
        if (player != null)
        {
            if (Vector2.Distance(transform.position, player.transform.position) <= viewDistanceVar) // Si el jugador esta cerca
            {
                Vector3 playerDir = (player.transform.position - transform.position).normalized;

                if (Vector3.Angle(rb.velocity, playerDir) < fovVar / 2)
                {
                    string[] collideWithThisLayers = new string[2] { "Player", "Wall" };
                    LayerMask collideWithThisMasks = LayerMask.GetMask(collideWithThisLayers);
                    RaycastHit2D ray = Physics2D.Raycast(transform.position, playerDir, viewDistanceVar, collideWithThisMasks); // Lanzar un raycast hacia el jugador

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
                            gfxAnimator.SetTrigger("Run"); // Actualizar animacion
                            gfx.gameObject.GetComponent<SpriteRenderer>().color = Color.red; // Cambiar color del sprite
                            gun.GetComponentInChildren<SpriteRenderer>().color = Color.red; // Cambiar color del sprite de la pistola
                            InvokeRepeating("UpdatePathToPlayer", 0, 0.2f); // Actualizar el Path hacia el player cada X segundos
                        }
                        followPlayerTimer = timeFollowingPlayer;
                    }
                }
            }
        }
    }

    void UpdateFollowParameters()
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
            gfxAnimator.SetTrigger("Idle");
            path = null; // Poner el path que se estaba siguiendo actualmente a null para que no persiga al jugador durante el "StunnedTime"
            Invoke("StunnedTime", 5); // Tiempo en el que el enemigo se queda parado sin hacer nada durante 2 segundos tras darse por vencido persiguiendo al jugador
        }
    }

    protected void UpdateGunRotation() // Rota la pistola para que apunte al jugador
    {
        if(player != null)
        {
            Vector2 diff = player.transform.position - transform.position;
            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg; // Calcular la rotacion para apuntar al jugador
            gun.rotation = Quaternion.Euler(0, 0, rot_z);
        }
       
    }

    protected virtual void Shoot()
    {
        Vector2 diff = player.transform.position - transform.position;
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg; // Calcular la rotacion para apuntar al jugador
        Rigidbody2D bulletInstance = Instantiate(bulletPrfb, shootPoint.position, Quaternion.Euler(0, 0, rot_z)).GetComponent<Rigidbody2D>();
        Destroy(bulletInstance, 5f);//seguro de destruccion

        player.GetComponent<SimpleMov>().TakeDamage();
    }

    void StunnedTime()
    {
        gfxAnimator.SetTrigger("Walk");
        gun.rotation = Quaternion.identity; // Poner pistola en posicion relajada
        gfx.gameObject.GetComponent<SpriteRenderer>().color = Color.yellow; // Cambiar color del sprite
        gun.GetComponentInChildren<SpriteRenderer>().color = Color.yellow; // Cambiar color del sprite de la pistola
        seeker.StartPath(rb.position, patrolPoints[patrolIndex].position, OnPathComplete); // Volver al punto en el que dejo la patrulla
    }

    public bool IsFollowPlayer()
    {
        return followPlayer;
    }

    public virtual void ChangeStats(float newDistance, float newFov, float newSpeed)
    {
        fovVar = newFov;
        viewDistanceVar = newDistance;
        speed = newSpeed;
        // Actualiza los valores del cono
        visionConeScript.SetFov(fovVar);
        visionConeScript.SetViewDistance(viewDistanceVar);
    }

    public virtual void Damage(bool byBullet) // Se pide el parametro "myBullet" para saber si el enemigo ha sido alcanzado por un ataque a meele o una bala
    {
        if (byBullet) DamageByBullet();
        else DamageByMeele();
    }

    void DamageByMeele()
    {
        path = null;
        gfxAnimator.SetTrigger("EnemyDeath");
        Destroy(visionConeScript.gameObject);
        Destroy(gun.gameObject);
        GetComponent<Collider2D>().enabled = false;
        Destroy(this.gameObject, 1.0f);

        int death = Random.Range(1, 4);
        if (death == 1) { FindObjectOfType<AudioManager>().Play("Player_Death"); }
        else if (death == 2) { FindObjectOfType<AudioManager>().Play("Enemy_Death1"); }
        else if (death == 3) { FindObjectOfType<AudioManager>().Play("Enemy_Death2"); }
        else if (death == 4) { FindObjectOfType<AudioManager>().Play("Enemy_Death3"); }
    }

    protected virtual void DamageByBullet()
    {
        path = null;
        gfxAnimator.SetTrigger("EnemyDeath");
        Destroy(visionConeScript.gameObject);
        Destroy(gun.gameObject);
        GetComponent<Collider2D>().enabled = false;
        Destroy(this.gameObject, 1.0f);

        int death = Random.Range(1, 4);
        if (death == 1) { FindObjectOfType<AudioManager>().Play("Player_Death"); }
        else if (death == 2) { FindObjectOfType<AudioManager>().Play("Enemy_Death1"); }
        else if (death == 3) { FindObjectOfType<AudioManager>().Play("Enemy_Death2"); }
        else if (death == 4) { FindObjectOfType<AudioManager>().Play("Enemy_Death3"); }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        LayerMask wallLayer = LayerMask.NameToLayer("Wall");
        if (collision.gameObject.layer == wallLayer) rb.velocity = Vector2.zero;
    }
}
