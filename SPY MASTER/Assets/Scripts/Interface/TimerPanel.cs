using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerPanel : MonoBehaviour
{
    [SerializeField]
    bool iniTime;
    Timer timer;
    void Start()
    {
        timer = Timer.GetInstance();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
