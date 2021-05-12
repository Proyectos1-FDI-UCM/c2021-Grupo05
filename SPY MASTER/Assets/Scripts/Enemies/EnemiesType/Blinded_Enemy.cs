// Nacho
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blinded_Enemy : EnemyAI
{
    [SerializeField]
    Transform rangeCircle;

    private void Start() 
    {
        // Actualizar circulo visible al jugador del rango
        UpdateCircleRange();
    }

    public override void ChangeStats(float newDistance, float newFov, float newSpeed)
    {
        fovVar = newFov;
        viewDistanceVar = newDistance;
        speed = newSpeed;
        // Actualiza los valores del cono
        visionConeScript.SetFov(fovVar);
        visionConeScript.SetViewDistance(viewDistanceVar);

        UpdateCircleRange();
    }


    void UpdateCircleRange()
    {
        Debug.Log("viewDistanceVar = " + viewDistanceVar);

        rangeCircle.localScale = new Vector3(viewDistanceVar * 2, viewDistanceVar * 2, 0);
    }

    protected override void CheckPlayer()
    {
        if (Vector2.Distance(transform.position, player.transform.position) <= viewDistanceVar) // Si el jugador esta cerca
        {
            if (cadenceTimer <= 0)
            {
                cadenceTimer = cadence;
                Debug.Log("PLAYER.TAKEDAMAGE()");
                Shoot();
            }

            if (!followPlayer) // Si todavia no se habia descubierto
            {
                followPlayer = true;
                Debug.Log("PLAYER.TAKEDAMAGE()");
                UpdateGunRotation(); // Actualizar la rotacion de la pistola antes de disparar con ella
                UpdateOrientation();
                Shoot();
                CancelInvoke(); // Cancelar otros invokes (En el caso en el que ya se estuviese persiguiendo al jugador y vuelve a ser detectado (reiniciar cuenta atras))
                speed = followSpeed;
                cadenceTimer = cadence;
                gfxAnimator.SetTrigger("Run"); // Actualizar animacion
                gfx.gameObject.GetComponent<SpriteRenderer>().color = Color.red; // Cambiar color del sprite
                gun.GetComponentInChildren<SpriteRenderer>().color = Color.red; // Cambiar color del sprite de la pistola
                InvokeRepeating("UpdatePathToPlayer", 0, 0.2f); // Actualizar el Path hacia el player cada X segundos
            }
            followPlayerTimer = timeFollowingPlayer;
        }
    }

    protected override void Shoot()
    {
        Vector3 playerDir = (player.transform.position - transform.position).normalized;

        // Hacer uso de un RayCast para que no dispare atraves de las paredes tambien (aparte de detectar al jugador)
        string[] collideWithThisLayers = new string[2] { "Player", "Wall" };
        LayerMask collideWithThisMasks = LayerMask.GetMask(collideWithThisLayers);
        RaycastHit2D ray = Physics2D.Raycast(transform.position, playerDir, viewDistanceVar, collideWithThisMasks); // Lanzar un raycast hacia el jugador

        if (ray.collider.gameObject.layer == 8) // Si el ray cast alcanza al jugador
        {
            Vector2 diff = player.transform.position - transform.position;
            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg; // Calcular la rotacion para apuntar al jugador
            Rigidbody2D bulletInstance = Instantiate(bulletPrfb, shootPoint.position, Quaternion.Euler(0, 0, rot_z)).GetComponent<Rigidbody2D>();
            Destroy(bulletInstance, 5f);//seguro de destruccion

            player.GetComponent<SimpleMov>().TakeDamage();
        }
    }
}
