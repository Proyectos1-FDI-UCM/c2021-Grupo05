using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Video;

public class VideoWait : MonoBehaviour
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
}
