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

        if (iniTime) timer.changeIni(Time.time);
    }

    // Update is called once per frame
    void Update()
    {
        timer.actPanel(text);
    }

    //para prueba de guardado
    public void SaveTime()
    {
        timer.SaveTime();
    }

    public void ShowTime()
    {
        timer.DebugTimes();
    }
}
