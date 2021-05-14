using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDDamage : MonoBehaviour
{
    [SerializeField]
    GameObject target;

    LayerMask layerObj;
    // Start is called before the first frame update

    // Update is called once per frame

    void Start()
    {
        layerObj = LayerMask.NameToLayer("Player");
    }
    void FixedUpdate()
    {
        RaycastHit hit;
        Vector3 tmp = target.transform.position - transform.position;

        if (Physics.Raycast(transform.position, transform.TransformDirection(tmp), out hit, Mathf.Infinity, layerObj))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(tmp) * hit.distance, Color.yellow);
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
