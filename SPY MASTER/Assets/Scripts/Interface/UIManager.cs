using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //Raúl Saavedra
    //Laura Gómez

    [SerializeField]
    Button pause;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.GetInstance().SetUiManager(this);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            //falta añadir la otra escena
            if(SceneManager.GetActiveScene().name == "MergeScene")
            
                pause.onClick.Invoke();
          
        }
    }

    public void CambiaEscena(string scene)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(scene);
    }
    public void PausaNivel(int valor)
    {
        Time.timeScale = valor;
    }
    public void ChangeTime()
    {
        int a;
        if (Time.timeScale == 0) a = 1;
        else a = 0;
        Time.timeScale = a;
    }


}
