using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoEnd : MonoBehaviour
{
    VideoPlayer videoPlay;
    public int sceneToChange;
    double length = 0.0f;

    private IEnumerator Start()
    {
        videoPlay = GetComponent<VideoPlayer>();
        length = videoPlay.clip.length;
        yield return new WaitForSeconds((float)length);
        SceneManager.LoadScene (sceneBuildIndex: sceneToChange);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene (sceneBuildIndex: sceneToChange);
        }
    }
}
