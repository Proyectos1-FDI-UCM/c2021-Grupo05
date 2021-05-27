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

    void Start()
    {
        GameManager.GetInstance().SetUiManager(this);


        if (SceneManager.GetActiveScene().name == "MenuPrincipal")
            AudioManager.GetInstance().Play("MainMenuMusic");

        if (SceneManager.GetActiveScene().name == "Level_1")
            AudioManager.GetInstance().Play("Gameplay");

    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            //falta añadir la otra escena
            if(SceneManager.GetActiveScene().name == "Level_1")
                pause.onClick.Invoke();
        }
    }

    public void CambiaEscena(string scene)
    {
        AudioManager.GetInstance().Stop("MainMenuMusic");
        AudioManager.GetInstance().Stop("Gameplay");

        Time.timeScale = 1;
        SceneManager.LoadScene(scene);
    }
    public void PausaNivel(int valor)
    {
        Time.timeScale = valor;
    }

    public void Exit()
    {
        Debug.Log("Exit");
        Application.Quit();
    }

    public void ChangeTime()
    {
        int a;
        if (Time.timeScale == 0) a = 1;
        else a = 0;
        Time.timeScale = a;
    }

    public void ClickSound()
    {
        AudioManager.GetInstance().Play("Stab");
    }
}
