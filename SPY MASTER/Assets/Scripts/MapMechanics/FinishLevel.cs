using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Jacobo
public class FinishLevel : MonoBehaviour
{
    public int sceneToChange = 3;
    
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(sceneBuildIndex: sceneToChange);
            Debug.Log("trigger");

            Timer.GetInstance().SaveTime();
        }
    }
}
