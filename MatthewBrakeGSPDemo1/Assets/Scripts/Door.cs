using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
   [SerializeField] private Transform previousRoom;
   [SerializeField] private Transform NextRoom;
    [SerializeField] private CameraMovement cam;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Ethan Sprite")
        {
            if (collision.transform.position.x > transform.position.x || collision.transform.position.y < transform.position.y) // checks which side of the collider
                //has been triggered to judge which way the camera needs to go
                cam.MoveToNewRoom(NextRoom);
            else
                cam.MoveToNewRoom(previousRoom);
        }
        Debug.Log("Entering Room: " + NextRoom);
    }

}
