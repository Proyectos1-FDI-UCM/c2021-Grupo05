using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDDamage : MonoBehaviour
{
    [SerializeField]
    GameObject target;
    [SerializeField]
    LineRenderer lr;

    [SerializeField]
    float velocityDamage;
    
    [SerializeField]
    LayerMask layerObj;
    // Start is called before the first frame update

    // Update is called once per frame
    float lastDamage = 0f;

    void Start()
    {
       
        lr.startColor = lr.endColor = Color.red;
        
    }
    void FixedUpdate()
    {
      
            Vector3 dir = (target.transform.position - transform.position).normalized;

            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, target.transform.position);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, layerObj);

        if (lastDamage + velocityDamage < Time.timeSinceLevelLoad)
        {
            if (hit != false)
            {
                SimpleMov player = hit.collider.gameObject.GetComponent<SimpleMov>();
                if (player != null)
                {
                    player.TakeDamage();
                    lastDamage = Time.timeSinceLevelLoad;
                }
            }
        }

            Debug.DrawLine(transform.position, hit.point, Color.red);
      
       
       
       // Debug.DrawLine(transform.position, transform.position + dir * 10, Color.red, Mathf.Infinity);
    }
    /*
     *  SimpleMov player = collision.gameObject.GetComponent<SimpleMov>();
        if(player != null)
        {
            player.TakeDamage();           
        }
    */
}
