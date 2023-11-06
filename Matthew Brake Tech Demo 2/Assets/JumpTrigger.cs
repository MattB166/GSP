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
        StartCoroutine(EndJump());
    }

    IEnumerator EndJump()
    {
        yield return new WaitForSeconds(1.5f);
        player.SetActive(true);
        jumpCam.SetActive(false);
    }
}

