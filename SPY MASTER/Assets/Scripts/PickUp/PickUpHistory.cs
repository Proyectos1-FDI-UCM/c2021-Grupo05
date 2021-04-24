
using UnityEngine;

public class PickUpHistory : MonoBehaviour
{
    [SerializeField]
     GameObject panel;

    [SerializeField]
    GameObject[] infos;

    [SerializeField]    
    bool isOpen;

    int i = 0;

  
    public void GetInfo()
    {
        if (isOpen)
        {
            if (i != 0) infos[i-1].SetActive(false);
            

            if (i != infos.Length)
            {
                infos[i].SetActive(true);
                i++;
            }  
            else
            {
                i = 0;
                isOpen = false;
                panel.SetActive(false);
            }
        }
        else
        {
            panel.SetActive(true);
            isOpen = true;
        }

    }

    public void Close()
    {
        panel.SetActive(false);
        isOpen = false;

        for (int i = 0; i < infos.Length; i++)
        {           
            infos[i].SetActive(false);
        }
    }

   
    
}
