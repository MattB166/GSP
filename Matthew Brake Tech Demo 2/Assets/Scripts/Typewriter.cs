using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Typewriter : MonoBehaviour
{

    public TextMeshProUGUI subtitleText;
    public string[] subtitles;
    public float delayBetweenSubtitles = 0.05f;

    private int currentSubtitleIndex = 0;

    private bool subtitlesStarted = false;
    public Action OnSubtitlesDone; 
    

   

    

    IEnumerator PlaySubtitles()
    {
        foreach(string subtitle in subtitles)   
        {
            subtitleText.text = " ";
            for (int i = 0; i < subtitle.Length; i++)
            {
                subtitleText.text += subtitle[i];
                yield return new WaitForSeconds(0.04f); //delays between each letter by specified amount
            }
            yield return new WaitForSeconds(delayBetweenSubtitles); //delay between each line 
            subtitleText.text = " ";
        }
            subtitlesStarted = false;

        OnSubtitlesDone?.Invoke(); //sends call to audio trigger to allow audio and subtitles to restart if within trigger 
        
    }

    public void StartSubtitles()
    {
        if (!subtitlesStarted)
        {
            currentSubtitleIndex = 0;
            subtitlesStarted = true;
            StartCoroutine(PlaySubtitles());
        }
    }
}
