using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


        rb.velocity = new Vector2(targetX - transform.position.x, targetY - transform.position.y).normalized * speed;
    }

    void IniForX()
    {
        iniPosX = transform.position.x;
        targetX = xTarget.transform.position.x;


    }
    void IniForY()
    {
      
        iniPosY = transform.position.y;
        targetX = yTarget.transform.position.y;

    }

    void ActX()
    {
        if (Mathf.Abs(transform.position.x - targetX) < 0.1f)
        {
            dirX = !dirX;
            if (dirX) targetX = xTarget.transform.position.x;
            else targetX = iniPosX;
        }
            
    }

    void ActY()
    {        
        if (Mathf.Abs(transform.position.y - targetY) < 0.1f)
        {
            dirY = !dirY;
            if (dirX) targetY = yTarget.transform.position.y;
            else targetY = iniPosY;
        }
            
    }

}
