using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
   public static AudioManager instance;

    [SerializeField]
    public SOUND[] sounds;                //audio manager in use due to various clips in the scene 


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach(SOUND s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }

    public void Play(string soundName)
    {
        SOUND s = Array.Find(sounds, sound => sound.name == soundName);
        if (s != null)
        {
            s.source.Play();
        }
        else
        {
            Debug.LogWarning("Sound with name: " + soundName + " Not found");
        }

    }
    public void Stop(string soundName)
    {
        SOUND s = Array.Find(sounds, sound => sound.name == soundName);
        {
            if(s != null)
            {
                s.source.Stop();
            }
            else
            {
                Debug.LogWarning("Sound not found");
            }
        }
    }
    
}


