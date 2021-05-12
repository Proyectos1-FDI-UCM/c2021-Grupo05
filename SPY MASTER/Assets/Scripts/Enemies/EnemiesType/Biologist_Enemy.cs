// Nacho
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Biologist_Enemy : EnemyAI
{
    public int buffRange;

    [HideInInspector]
    public List<EnemyAI> enemiesInRange; // Almacena todos los enemigos que esten cerca 

    protected override void UpdatePathToPlayer() // Huir del jugador
    {
        // Calcular direccion unversa al jugador y restarsela a la posicion actual
        Vector3 playerDirection = target.transform.position - transform.position;

        // Crear el path, donde hay que aclarar desde que posicion, hacia que posicion, y la funcion que queremos que invoque al terminar de calcularla
        if (seeker.IsDone()) // Si no se esta calculando un path actualmente
            seeker.StartPath(rb.position, transform.position - playerDirection, OnPathComplete);
    }

    protected override void UpdateVisionCone()
    {
        if (followPlayer) // Cuando detecte al jugador, que se aleje de el mientras va mirando hacia atras
        {
            // Actualiza los valores del Cono de vision para que este se dibuje correctamente
            visionConeScript.SetOrigin(transform.position);
            if (rb.velocity != Vector2.zero)
                visionConeScript.SetDirection(-rb.velocity);
        }
        else 
        {
            // Actualiza los valores del Cono de vision para que este se dibuje correctamente
            visionConeScript.SetOrigin(transform.position);
            if (rb.velocity != Vector2.zero)
                visionConeScript.SetDirection(rb.velocity);
        }
    }

    protected override void Shoot() { } // No disparar al jugador
}
