using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class mouseLook : MonoBehaviour
{
    public float mouseSensitivity = 500f;
    public Transform playerBody;
    private float xRotation = 0f;
    private float targetHeightOffset = 4f;
    private float targetWidthOffset = 5f;
    Quaternion initialCameraRot;
    
    
    // Start is called before the first frame update
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        initialCameraRot = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
     
        
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
            playerBody.Rotate(Vector3.up * mouseX);
        
        
    }

    public void PanCameraToTarget(Transform target, float panSpeed)
    {
        StartCoroutine(PanCamera(target, panSpeed));
    }

    private IEnumerator PanCamera(Transform target, float panSpeed)
    {
       // Quaternion startRot = transform.localRotation;
        Vector3 originalPos = playerBody.position;
        Quaternion originalRot = playerBody.rotation;

        Vector3 targetPos = target.position + Vector3.up * targetHeightOffset + target.right * targetWidthOffset;

       
        float startTime = Time.time;
        float journeyDuration = Vector3.Distance(originalPos,targetPos) / panSpeed;

        

        while(Time.time < startTime + journeyDuration)
        {
           
            float fractionOfJourney = (Time.time - startTime) / journeyDuration;
            playerBody.rotation = Quaternion.Slerp(originalRot, Quaternion.LookRotation(targetPos - originalPos), fractionOfJourney);
            

           
            yield return null;
           
        }

        playerBody.rotation = Quaternion.LookRotation(targetPos - transform.position);
       
        

    }





}
