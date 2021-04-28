using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //GameManager.GetInstance
    }

    public void CambiaEscena(string scene)
    {
        SceneManager.LoadScene(scene);
    }

 
}
