//Jacobo
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class LookPlayer : MonoBehaviour
{
    ///<summary>
    ///Rota SpriteRenderer(requerido) dependiendo de la posicion de cursor.
    ///Generalizar en un futuro con ambos axis? 
    ///</summary>

    public enum Target{ Player, Other} // No tiene mas que un uso actualmente
    [Header("Mirar a:")]
    public Target target;

    private Transform targetToLook;
    SpriteRenderer spriteRend;
    IEnumerator Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();
        switch (target)
        {
            case Target.Player: targetToLook = GameObject.Find("Player").transform; break;
            case Target.Other: targetToLook = GameObject.Find("Player").transform; break;
        }
        while(true)
        {
            spriteRend.flipX = targetToLook.position[0] < transform.position[0] ? true : false;
            yield return new WaitForSeconds(0.2f);
        }
    }
}
