//Jacobo, Nacho
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMov : MonoBehaviour
{
    ///<summary>
    ///Clase provisional. Reemplazar con movimiento mas adelante. Conservar apuntado, balas.
    ///Heredar de player? Clase static // 
    ///Limpiar referencias en inspector, GetChild, etc.
    ///</summary>

    public Transform bulletPrefab;

    Rigidbody2D body;
    float horizontal;
    float vertical;
    Vector2 moveDir;

    [Header("Stats")]
    [SerializeField]
    [Range(5.0f, 15.0f)]
    private float runSpeed = 7.0f;

    [Header("References")]
    [SerializeField]
    private Transform HandPivot;
    [SerializeField]
    private Detector enemDetector;
    private Transform aimPoint;
    private Animator gfxAnim;
    private Transform playerSpr;

    [SerializeField]
    int maxHealth = 3;
    int health;

    [SerializeField]
    int maxBullets = 3;
    int numOfBullets;

    [SerializeField]
    HudCountController bulletHud;
    [SerializeField]
    HudCountController healthHud;

    void Start()
    {
        //References (LIMPIAR)
        playerSpr = transform.GetChild(0).transform;
        aimPoint = HandPivot.GetChild(0).GetChild(0).GetChild(0).transform;
        gfxAnim = GetComponentInChildren<Animator>();

        // Health
        health = maxHealth;

        //Bullets
        numOfBullets = maxBullets;
        GetAmmo(); // Testing

        //Add rb
        if (!GetComponent<Rigidbody2D>())
        {
            gameObject.AddComponent<Rigidbody2D>();
        }
        body = GetComponent<Rigidbody2D>();

        //Visuals Coroutine
        StartCoroutine(CheckForMousePos());
    }

    IEnumerator CheckForMousePos()
    {
        //Check mouse Pos and applies rot to playerSpr & handSpr(ONLY VISUALS)
        Transform gunPivot = HandPivot.GetChild(0); // Access 
        while (true)
        {
            //BODY ROTATION
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            int rotToApply = mousePos[0] < transform.position[0] ? 180 : 0;
            playerSpr.rotation = new Quaternion(0, rotToApply, 0, 1);

            //HAND ROTATION
            if (mousePos.x > transform.position.x) gunPivot.localScale = new Vector2(1, 1);
            else gunPivot.localScale = new Vector2(1, -1);

            yield return new WaitForSeconds(0.05f); //<- no optimizado, correr todo en Update?
        }
    }

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;

        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos.x -= objectPos.x;
        mousePos.y -= objectPos.y;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        HandPivot.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        //Click Izquierdo // DISPARO
        if (Input.GetMouseButtonDown(0) && numOfBullets > 0 && Time.timeScale != 0)
        {
            Rigidbody2D bulletInstance = Instantiate(bulletPrefab, aimPoint.position, aimPoint.rotation).GetComponent<Rigidbody2D>();
            Destroy(bulletInstance, 5f);//seguro de destruccion
            numOfBullets--;
            bulletHud.HudLess(1);

            AudioManager.GetInstance().Play("Silenced_shot");
            //Se aplica aceleracion con ForceMode2D.Force, no con Impulse. No utilizamos masa, creo?
        }

        //Click Derecho // MEELE
        if (Input.GetMouseButtonDown(1) && Time.timeScale != 0)
        {
            /////////////////////////////////////////////////////////////////////////////gfxAnim.Play("MeleeAttackAnim");
            Transform enemyInRange = enemDetector.GetNearestEnemy();
            
            if (enemyInRange != null && Vector3.Distance(transform.position, enemyInRange.position) < 2) //Variable en un futuro
            {
                Debug.Log("EnemyDetected");
                enemyInRange.GetComponent<EnemyAI>().Damage(false);
                AudioManager.GetInstance().Play("Stab");
            }
            //detection
        }

        //Draw Bullet Gizmo Direction
        //Debug.DrawRay(handGunSpr.position, handGunSpr.right * 50f, Color.red);

        if (Input.GetKeyDown("r"))
        {
            GetAmmo();
            bulletHud.currentCount = 3;
            bulletHud.UploadHud();
        }

        gfxAnim.SetFloat("Velocity", body.velocity.magnitude);
    }

    void FixedUpdate()
    {
        //Movimiento 
        moveDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveDir.Normalize();
        body.velocity = moveDir * runSpeed;
        
    }

    public void GetAmmo()
    {
        numOfBullets = 3;
        bulletHud.currentCount = 3;
        bulletHud.UploadHud();

    }
    public void GetLive()
    {
        health++;//Mathf.Infinity;
        healthHud.HudAdd(1);
        //numOfBullets = 999;//Mathf.Infinity;
    }

    public bool CheckMaxLive()
    {
        return maxHealth > health;
    }

    public bool CheckBullets()
    {
        Debug.Log("numOfBullets = " + numOfBullets);

        return numOfBullets != 3;
    }

    public void TakeDamage()
    {
        health--;
        healthHud.HudLess(1);
        AudioManager.GetInstance().Play("Damaged");
        if (health == 0)
        {
            Time.timeScale = 0;
            GameManager.GetInstance().GameOver();
            Destroy(gameObject);

            AudioManager.GetInstance().Play("Player_Death"); 
            
        }
    }
}
