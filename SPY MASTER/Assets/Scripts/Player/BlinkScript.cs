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
    Slider blinkBarSlider;

    [SerializeField]
    GameObject SpriteDebug;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        blinkCharge = maxBlinkCharge;
        blinkBarSlider.maxValue = maxBlinkCharge;
    }
    private void Update()
    {
        // Movimiento basico

        //int xAxis = (int) Input.GetAxisRaw("Horizontal");
        //int yAxis = (int) Input.GetAxisRaw("Vertical");

        //rb.velocity = new Vector2(xAxis, yAxis) * speed;


        
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Blink();
            FindObjectOfType<AudioManager>().Play("Blink");
        }
            

        // Incrementar Charge del blink
        if (blinkCharge + blinkChargeSpeed * Time.deltaTime <= maxBlinkCharge)
            blinkCharge += blinkChargeSpeed * Time.deltaTime;
        else blinkCharge = maxBlinkCharge;

        // Actualizar slider mostrando el charge del Blink
        blinkBarSlider.value = blinkCharge;
    }

    void Blink ()
    {
        // Calcular la posicion a la que se intenta teletransportar
        Vector2 mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        Vector3 mouseDirection = new Vector3(mousePos.x - transform.position.x, mousePos.y - transform.position.y, 0).normalized;

        Vector2 newPosition = transform.position + mouseDirection * blinkDistance;

        if (blinkCharge - 1 >= 0 && !Physics2D.OverlapCircle(newPosition, transform.localScale.x / 2, LayerMask.GetMask("Wall")))
        {
            transform.position = newPosition;
            blinkCharge -= 1;
        }
        else if (SpriteDebug != null) SpriteDebug.transform.position = newPosition;
    }
}
