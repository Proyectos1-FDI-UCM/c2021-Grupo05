using UnityEngine;
//Raúl Saavedra de la Riera
public class LaserMov : MonoBehaviour
{
    [SerializeField]
    float distanceMovX = 0f;
    [SerializeField]
    float distanceMovY = 0f;
    [SerializeField]
    float speedX = 0f;
    [SerializeField]
    float speedY = 0f;
    [SerializeField]
    bool xAxis = false;
    [SerializeField]
    bool yAxis = false;

    float realDisX, iniPosX,
        realDisY, iniPosY;

    bool movX, movY;


    // Start is called before the first frame update
    void Start()
    {
        iniPosX = transform.position.x;
        realDisX = distanceMovX + transform.position.x;

        iniPosY = transform.position.y;
        realDisY = distanceMovY + transform.position.y;

        if (xAxis)
        movX = true;
        if (yAxis)
        movY = true;

    }

    // Update is called once per frame
    void Update()
    {
        float x, y;
        x = transform.position.x;
        y = transform.position.y;


        float targetX, targetY;
        if (xAxis)
        {
            if (movX) targetX = realDisX;
            else targetX = iniPosX;
            if (xAxis) x = Mathf.MoveTowards(transform.position.x, targetX, speedX * Time.deltaTime);
            if (Mathf.Abs(targetX - transform.position.x) < 0.1f) movX = !movX;
        }
       if(yAxis)
        {
            if (movY) targetY = realDisY;
            else targetY = iniPosY;
            if (yAxis) y = Mathf.MoveTowards(transform.position.y, targetY, speedY * Time.deltaTime);
            if (Mathf.Abs(targetY - transform.position.y) < 0.1f) movY = !movY;
        }   
        transform.position = new Vector2(x, y);

       
        

    }
}
