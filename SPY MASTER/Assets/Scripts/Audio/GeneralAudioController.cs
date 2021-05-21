using UnityEngine.UI;
using UnityEngine;
//Raúl Saavedra 
public class GeneralAudioController : MonoBehaviour
{
    [SerializeField]
    Slider bar;

    // Start is called before the first frame update
    void Start()
    {
       float iniVol = PlayerPrefs.GetFloat("volume");
        if (iniVol == 0) iniVol = 0.5f;

        AudioListener.volume = bar.value = iniVol;
    }

   

    public void ChangeVolume()
    {
        float volume = bar.value;
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("volume", volume);
    }
}
