using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    public AudioClip cliptoPlay;
    public Typewriter typewriter; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            Debug.Log("Player collided");
            Audio.instance.Say(cliptoPlay);
            typewriter.StartSubtitles();
        
    }
}
