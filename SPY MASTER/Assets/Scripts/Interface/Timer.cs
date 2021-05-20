using UnityEngine.UI;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField]
    Text text;
    float iniTime;
    GameManager gm;

    private static Timer instance;

    int min, seconds;
    // Start is called before the first frame update

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

    void Start()
    {
        gm = GameManager.GetInstance();
    }

    // Update is called once per frame
    void Update()
    {
        float actTime = Time.time - iniTime;
        min = (int)actTime / 60;
        seconds = (int)actTime % 60;

        text.text = min + " : " + seconds;
    }

    public static Timer GetInstance(float actTime)
    {
        return instance;
    }


    public void AddTime(float time)
    {
      iniTime =  iniTime - time;
    }

    public void SaveTime()
    {
        int mark = min * 60 + seconds;

        int first, second, third;
        
        first = PlayerPrefs.GetInt("first");
        second = PlayerPrefs.GetInt("second");
        third = PlayerPrefs.GetInt("third");
       

        if(first != 0) PlayerPrefs.SetInt("first", mark);
        else if(second != 0) PlayerPrefs.SetInt("second", mark);
        else if(third != 0) PlayerPrefs.SetInt("third", mark);
        else
        {
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
        }
                        
    }

}
