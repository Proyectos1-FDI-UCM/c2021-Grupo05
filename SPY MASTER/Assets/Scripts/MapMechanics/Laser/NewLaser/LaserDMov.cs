using UnityEngine;
//Raúl Saavedra de la Riera
public class LaserDMov : MonoBehaviour
{
   
    [SerializeField]
    float speed = 0f;   
    [SerializeField]
    bool xAxis = false;
    [SerializeField]
    bool yAxis = false;

    [SerializeField]
    GameObject xTarget;
    [SerializeField]
    GameObject yTarget;

    Rigidbody2D rb;

    float realDisX, iniPosX,
       realDisY, iniPosY;

    bool dirX, dirY;
    float targetX, targetY;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (xAxis) IniForX();
        if (yAxis) IniForY();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (xAxis)       
            ActX();
            
        
        if (yAxis)        
            ActY();

        Vector2 tmp = new Vector2(0,0);
        if(xAxis)tmp += new Vector2(targetX - transform.position.x, 0).normalized;
        if(yAxis) tmp += new Vector2(0, targetY - transform.position.y).normalized;

        rb.velocity = tmp * speed;
    }

    void IniForX()
    {
        iniPosX = transform.position.x;
        targetX = xTarget.transform.position.x;


    }
    void IniForY()
    {
      
        iniPosY = transform.position.y;
        targetY = yTarget.transform.position.y;

    }

    void ActX()
    {
        if (Mathf.Abs(targetX - transform.position.x) < 0.1f)
        {
            dirX = !dirX;
            if (dirX) targetX = xTarget.transform.position.x;
            else targetX = iniPosX;
        }
            
    }

    void ActY()
    {        
        if (Mathf.Abs(targetY - transform.position.y) < 0.1f)
        {
            dirY = !dirY;
            if (dirY) targetY = yTarget.transform.position.y;
            else targetY = iniPosY;
        }
            
    }

}
