using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    [SerializeField] private float speed;

    private float currentPosX;
    private float currentPosY;
    private Vector3 velocity = Vector3.zero;
    public GameObject room1;

    private void Awake()
    {
        currentPosX = room1.transform.position.x;
        currentPosY = room1.transform.position.y;
    }


    [SerializeField] private Transform player;
    private void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentPosX, currentPosY, transform.position.z), ref velocity, speed);
        //transform.position = new Vector3(player.position.x, player.position.y, player.position.z);
    }

    
    public void MoveToNewRoom(Transform newroom)
    {
        currentPosX = newroom.position.x; //sets new x and y positions to centre of the new room 
        currentPosY  = newroom.position.y;
    }

}
