using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLevel : MonoBehaviour
{
    public int sceneToChange = 3;
    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Player"))    
        {
            SceneManager.LoadScene(sceneBuildIndex: sceneToChange);
        }
    }
}
