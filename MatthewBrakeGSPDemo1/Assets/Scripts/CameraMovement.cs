using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    [SerializeField] private float speed;

    private float currentPosX;
    private Vector3 velocity = Vector3.zero;

    private void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentPosX, transform.position.y, transform.position.z), ref velocity, speed * Time.deltaTime);
    }


    public void MoveToNewRoom(Transform newroom)
    {
        currentPosX = newroom.position.x;
    }

}