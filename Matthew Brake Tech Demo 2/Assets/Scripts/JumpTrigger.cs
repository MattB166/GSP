using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTrigger : MonoBehaviour
{
    public GameObject player;
    public GameObject jumpCam;

    private void OnTriggerEnter(Collider other)
    {
       jumpCam.SetActive(true);
       player.SetActive(true);
        AudioManager.instance.Stop("LevelMusic");
        AudioManager.instance.Play("JumpScare");
        StartCoroutine(EndJump());
    }

    IEnumerator EndJump()
    {
        yield return new WaitForSeconds(2f);
        AudioManager.instance.Stop("JumpScare");
        AudioManager.instance.Play("LevelMusic");
        player.SetActive(true);
        jumpCam.SetActive(false);
    }
}

