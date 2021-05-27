using System.Collections;
using UnityEngine.UI;
using UnityEngine;
//Raul Saavedra
//Laura Gómez
public class TimerPanel : MonoBehaviour
{
    [SerializeField]
    bool iniTime;

    [SerializeField]
    Text text;

    Timer timer;
    void Start()
    {
        
        timer = Timer.GetInstance();

        if (timer != null && iniTime) timer.changeIni(Time.time);
    }

    // Update is called once per frame
    void Update()
    {
        if(timer != null)
        timer.actPanel(text);
    }

    //para prueba de guardado
  
}
