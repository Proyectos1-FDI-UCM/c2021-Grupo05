//Isidro Lucas
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    static AudioManager instance;

    
    static public AudioManager GetInstance()
    {  
        return instance;
    }

    void Awake()
    {

        //Para que cuando haya música no se corte al cambiar de escena
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    // Update is called once per frame
    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            //Debug.Log("Sound " + name + " not found");
            return;
        }
        //Debug.Log("Sound " + name + " found");
        s.source.Play();
    }

   
}
