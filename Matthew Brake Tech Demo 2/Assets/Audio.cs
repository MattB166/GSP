using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    private AudioSource source;
    public static Audio instance;
    
    private void Awake()
    {
        instance = this; 
    }

    private void Start()
    {
        source = gameObject.AddComponent<AudioSource>();
    }

    public void Say(AudioClip clip)
    {
        if(source.isPlaying)
            source.Stop();

        source.PlayOneShot(clip);
    }
}
