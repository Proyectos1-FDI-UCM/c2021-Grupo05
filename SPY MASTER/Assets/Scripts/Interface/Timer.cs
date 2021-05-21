using UnityEngine;
using UnityEngine.UI;
//Raúl Saavedra
public class Timer : MonoBehaviour
{
    private static Timer instance;

    float iniTime;
  

    int min, seconds;
    // Start is called before the first frame update

    private void Awake()
    {
        //PlayerPrefs.DeleteAll();
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

    

    public static Timer GetInstance()
    {         
        return instance;
    }

    public void changeIni(float actTime)
    {
        iniTime = actTime;
    }


   public void actPanel(Text text)
    {
        float actTime = Time.time - iniTime;
        min = (int)actTime / 60;
        seconds = (int)actTime % 60;

        text.text = min + " : " + seconds;
    }

    public void SaveTime()
    {
        int mark = min * 60 + seconds;
        Debug.Log("current mark: " + mark);

        int first, second, third;
        
        first = PlayerPrefs.GetInt("first");
        second = PlayerPrefs.GetInt("second");
        third = PlayerPrefs.GetInt("third");

        Debug.Log("first" + first);
        Debug.Log("second" + second);
        Debug.Log("third" + third);

        
        
            if (first > mark)
            {
                PlayerPrefs.SetInt("first", mark);
                PlayerPrefs.SetInt("second", first);
                PlayerPrefs.SetInt("third", second);
            }

            else if (second > mark)
            {
                PlayerPrefs.SetInt("second", mark);
                PlayerPrefs.SetInt("third", second);
            }

            else if (third > mark)
            {
                PlayerPrefs.SetInt("third", mark);
            }
            else
            
            if (first == 0) PlayerPrefs.SetInt("first", mark);
            else if (second == 0) PlayerPrefs.SetInt("second", mark);
            else if (third == 0) PlayerPrefs.SetInt("third", mark);
    }
       
                        
    

    //para probar guarda tiempo

    public void DebugTimes()
    {
        int first, second, third;

        first = PlayerPrefs.GetInt("first");
        second = PlayerPrefs.GetInt("second");
        third = PlayerPrefs.GetInt("third");

        Debug.Log(first);
        Debug.Log(second);
        Debug.Log(third);
    }


}
