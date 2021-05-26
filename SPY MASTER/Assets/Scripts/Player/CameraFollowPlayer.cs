// Jacobo
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    private Transform player;

    void Start()
    {
        player = transform.parent;
        transform.parent = null;
    }

    private void Update()
    {
        if(player != null)
       transform.position = Vector3.Lerp(transform.position, player.position + new Vector3(0,0,-10), Time.deltaTime * 3.2f);
    }
}
