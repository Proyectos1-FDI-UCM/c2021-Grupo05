using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
//Raúl Saavedra
public class MenuScripts : MonoBehaviour
{
  
    [SerializeField]
    Text mark1, mark2, mark3;
    // Start is called before the first frame update
    public void Exit()
    {
        Application.Quit();
    }


    public void ChangeScene(string name)
    {
        SceneManager.LoadScene(name);
    }
    


    public void ShowResult(int i)
    {
        Text currentText = mark1;

        float mark = 0;
        if (i == 1)
        {
            mark = PlayerPrefs.GetInt("first");
           
        }
        if (i == 2)
        {
            mark = PlayerPrefs.GetInt("second");
            currentText = mark2;
        }
        if (i == 3)
        {
            mark = PlayerPrefs.GetInt("third");
            currentText = mark3;
        }

        Debug.Log(mark);
        string toWrite;
        if (mark != 0)
        {
            int min = (int)mark / 60;
            int sec = (int)mark % 60;
            toWrite = min + " : " + sec;
        }
        else toWrite = "---";

        currentText.text = toWrite;
    }
}

