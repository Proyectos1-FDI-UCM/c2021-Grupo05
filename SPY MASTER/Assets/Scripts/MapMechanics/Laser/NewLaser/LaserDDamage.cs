using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDDamage : MonoBehaviour
{
    [SerializeField]
    GameObject target;
    [SerializeField]
    LineRenderer lr;

    LayerMask layerObj;
    // Start is called before the first frame update

    // Update is called once per frame

    void Start()
    {
        layerObj = LayerMask.NameToLayer("Player");
        lr.startColor = lr.endColor = Color.red;
        
    }
    void FixedUpdate()
    {
        RaycastHit hit;
        Vector3 tmp = target.transform.position - transform.position;

        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, target.transform.position);
        if (Physics.Raycast(transform.position, transform.TransformDirection(tmp), out hit, Mathf.Infinity, layerObj))
        {           
            Debug.Log("Did Hit");
        }
      
    }
    /*
     *  SimpleMov player = collision.gameObject.GetComponent<SimpleMov>();
        if(player != null)
        {
            player.TakeDamage();           
        }
    */
}
