using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{
    Transform player;

    [SerializeField]
    Transform playerPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player = collision.transform;

            GetComponent<Animator>().SetBool("ActivateTrans", true);

            Invoke("Teleport", 1.15f);
        }
    }

    void Teleport()
    {
        player.position = playerPoint.position;
    }

}
