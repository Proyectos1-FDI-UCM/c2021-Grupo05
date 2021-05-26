// Nacho
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkScript : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField]
    float speed;
    
    [SerializeField]
    float blinkDistance;
    [SerializeField]
    float maxBlinkCharge;
    float blinkCharge;
    [SerializeField]
    float blinkChargeSpeed;

    [SerializeField]
    Image blinkBarSlider;

    [SerializeField]
    GameObject SpriteDebug;

    private Animator anim;

    Collider2D col;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        blinkCharge = maxBlinkCharge;
        anim = transform.GetComponentInChildren<Animator>();
        col = GetComponent<Collider2D>();
    }
    private void Update()
    {
        // Movimiento basico
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartCoroutine(Blink());
        }

        // Incrementar Charge del blink
        if (blinkCharge + blinkChargeSpeed * Time.deltaTime <= maxBlinkCharge)
            blinkCharge += blinkChargeSpeed * Time.deltaTime;
        else blinkCharge = maxBlinkCharge;

        // Actualizar slider mostrando el charge del Blink
        blinkBarSlider.fillAmount = blinkCharge/3;
    }

    IEnumerator Blink()
    {
        // Calcular la posicion a la que se intenta teletransportar
        Vector2 mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        Vector3 mouseDirection = new Vector3(mousePos.x - transform.position.x, mousePos.y - transform.position.y, 0).normalized;

        Vector2 newPosition = transform.position + mouseDirection * blinkDistance;

        if (blinkCharge - 1 >= 0 && !Physics2D.OverlapCircle(newPosition, transform.localScale.x / 2, LayerMask.GetMask("Wall")))
        {
            //Tiempo e invulnerabilidad
            col.enabled = false;
            anim.Play("Player_Blink");
            yield return new WaitForSeconds(0.3f);
            rb.position = newPosition;
            blinkCharge -= 1;
            AudioManager.GetInstance().Play("Blink");
            yield return new WaitForSeconds(0.3f);
            col.enabled = true;
        }
        else if (SpriteDebug != null) SpriteDebug.transform.position = newPosition;
    }
}
