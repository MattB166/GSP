using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Typewriter : MonoBehaviour
{

    public TextMeshProUGUI subtitleText;
    public string[] subtitles;
    public float delayBetweenSubtitles = 1.0f;

    private int currentSubtitleIndex = 0;

    private bool subtitlesStarted = false;
    //private bool subtitlesPlaying = false;

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(PlaySubtitles());  //plays it from beginning. dont want that 
    }

    //private void Update()
    //{
    ////    if (subtitlesStarted)
    ////        StartCoroutine(PlaySubtitles());
    ////}

    IEnumerator PlaySubtitles()
    {
        foreach(string subtitle in subtitles)
        {
            subtitleText.text = " ";
            for (int i = 0; i < subtitle.Length; i++)
            {
                subtitleText.text += subtitle[i];
                yield return new WaitForSeconds(0.05f);
            }
            yield return new WaitForSeconds(delayBetweenSubtitles);
            subtitleText.text = " ";
            subtitlesStarted = false;
        }
        
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
