using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    Vector3 velocity; 
    public float gravity = -9.81f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    private bool isGrounded;
    [SerializeField] private GameObject Inventory; 
    
    // Start is called before the first frame update
    void Start()
    {
        Inventory.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");


        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity  * Time.deltaTime);

        if(Input.GetKeyDown(KeyCode.I))
        {
            if (Inventory != null)
            {
                if (Inventory.activeSelf)
                {
                    Inventory.SetActive(false);
                    Time.timeScale = 1f;
                }
                   
                else
                {
                    Inventory.SetActive(true);
                     Time.timeScale = 0f; 

                }
            }
            else
                Debug.Log("Inventory could not be found");
           
            
            
        }



    }
}
