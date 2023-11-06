using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable] //allows all audio clips to be modifiable 
public class SOUND
{
    public AudioClip clip;

    public string name;

    [Range(0f,1f)]
    public float volume;
    [Range (0f,3f)]
    public float pitch;

    [HideInInspector]
    public AudioSource source;
}

