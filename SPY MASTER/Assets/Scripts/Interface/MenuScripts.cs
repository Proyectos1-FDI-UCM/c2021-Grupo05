using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScripts : MonoBehaviour
{
    // Start is called before the first frame update
    public void Exit()
    {
        Application.Quit();
    }


    public void ChangeScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}

