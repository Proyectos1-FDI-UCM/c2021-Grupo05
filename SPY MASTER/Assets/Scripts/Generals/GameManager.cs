using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Variable que almacena la instancia del GameManager
    private static GameManager instance;
    /*Al activar el objeto asociado, evita que haya dos controladores del GameManager*/
    private void Awake()
    {
        //Si no tiene instancia ya creada, almacena la actual
        if (instance == null)
        {
            instance = this;

            //No destruye el Game Manager aunque se cambie de escena  
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            //Si ya hay instancia creada lo destruye porque no se necesita otro
            Destroy(this);
        }
    }
    //Devuelve la instancia del GameManager
    public static GameManager GetInstance()
    {
        return instance;
    }

    //Asignador del uimanager
    public void SetUiManager(UIManager uim)
    {

    }
    //Método para abandonar la partida
    public void Exit()
    {
        Application.Quit();
    }

}
