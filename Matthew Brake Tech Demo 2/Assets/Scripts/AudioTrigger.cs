using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    public AudioClip cliptoPlay;
    public Typewriter typewriter;
    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            
                Debug.Log("Player collided");
                Audio.instance.Say(cliptoPlay);
                typewriter.StartSubtitles();
            typewriter.OnSubtitlesDone += AllowAudioTrigger;

                hasTriggered = true;
        }
    }

    private void AllowAudioTrigger()
    {
        hasTriggered = false;
    }
}
