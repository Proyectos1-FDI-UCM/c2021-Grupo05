//Jacobo
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    ///<summary>
    ///Selecciona al enemigo mas proximo al jugador dentro del radio de deteccion(con Tag = "Enemy").
    ///Asigna a ese enemigo el canvas de asesinato (Canvas_Melee) para que el jugador sepa a quien puede matar(VISUAL)
    ///Conversion radio/unidades => radio Canvas = 40x40 == 75u~
    ///</summary>

    //Canvas Melee
    [SerializeField]
    Transform meleeCanvas;

    //Lista de posibles enemigos en rango
    private List<Transform> enemInRange = new List<Transform>(0);
    private CanvasGroup canvasAlpha;
    private RectTransform canvasRect;

    private void Start() 
    {
        canvasAlpha = meleeCanvas.GetComponent<CanvasGroup>();
        canvasRect = meleeCanvas.GetComponent<RectTransform>();
        meleeCanvas.SetParent(null);
    }

    private void OnTriggerEnter2D(Collider2D col) //usar if(col is Soldier) en un Futuro?
    {
        //if (col.GetComponent(typeof(EnemyAI)) as EnemyAI != null)
        if(col.transform.CompareTag("Enemy"))
        {
            enemInRange.Add(col.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D col) //Anadir extra condicion si se puede desabilitar detector
    {
        //if (col.GetComponent(typeof(EnemyAI)) as EnemyAI != null)
        if(col.transform.CompareTag("Enemy"))
        {
            enemInRange.Remove(col.transform);
        }
    }

    private void Update() 
    {
        canvasAlpha.alpha = enemInRange.Count > 0 ? 1 : 0;
        if (enemInRange.Count > 0)
        {
            meleeCanvas.SetParent(GetNearestEnemy(), true);
            canvasRect.anchoredPosition = new Vector3(0,0,0);
        }
        //Al morir los enemigos desactivan collider y esperan un lapso de animacion de muerte.
        //Detach el canvas en ese momento para no ser eliminado con los enemigos (va a ver que cambiar esto si o si /con eventos?)
        else
        {
            meleeCanvas.SetParent(null, true);
        }
    }

    public Transform GetNearestEnemy()
    {
        float minDistance = Mathf.Infinity;
        float distanceBetween;
        Transform enemToReturn = null;
        foreach (Transform enem in enemInRange)
        {
            distanceBetween = Vector3.Distance(transform.position, enem.position);
            if(distanceBetween < minDistance)
            {
                minDistance = distanceBetween;
                enemToReturn = enem;
            }
        }
        return enemToReturn;
    }
}
