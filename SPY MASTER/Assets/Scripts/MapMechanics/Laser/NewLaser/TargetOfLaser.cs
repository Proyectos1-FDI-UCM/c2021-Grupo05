using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Raúl Saavedra de la Riera
public class TargetOfLaser : MonoBehaviour
{
    [Header("Indicador rojo")]
    [SerializeField]
    bool red;
    [Header("Indicador azul")]
    [SerializeField]
    bool blue;

    private void OnDrawGizmos()
    {
        if(blue)
        Gizmos.color = Color.blue;
        if (red) 
        Gizmos.color = Color.red;
        Gizmos.DrawCube(this.gameObject.transform.position, new Vector2(0.25f,0.25f));
    }
}
