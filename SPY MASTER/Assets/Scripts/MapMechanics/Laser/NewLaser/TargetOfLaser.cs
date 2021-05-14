using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Raúl Saavedra de la Riera
public class TargetOfLaser : MonoBehaviour
{
  
    private void OnDrawGizmos()
    {
     
        Gizmos.color = Color.red;
        Gizmos.DrawCube(this.gameObject.transform.position, new Vector2(0.25f,0.25f));
    }
}
