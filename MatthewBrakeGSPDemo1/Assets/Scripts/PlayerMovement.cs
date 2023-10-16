using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static Unity.Burst.Intrinsics.X86.Avx;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    public float speed = 8f;
    public float rotationSpeed = 5f;
    public float jumpingPower = 16f;
    private bool isFacingRight = true;
    public bool axeThrown = false;
    private float jetForce = 45f;
    private float fuel = 100f;
    private float currentFuelAmount;
    private bool haveFuel = true;
    private float timer = 0;
    private Vector3 startpos; 


    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private GameObject axePrefab; 
    [SerializeField] private bool isJetPackActive = false;
    private Quaternion targetRot = Quaternion.identity;
    [SerializeField] private Slider FuelSlider;
    [SerializeField] private float fuelBurnRate = 20f;
    [SerializeField] private float fuelRefillRate = 10f;
    [SerializeField] private float refillCoolDown = 2f; 

    private SpriteRenderer spriteRenderer; //used as jump animation only had one side 
    

    public Animator animator;

    private void Awake()
    {
         spriteRenderer = GetComponent<SpriteRenderer>();
        currentFuelAmount = fuel; 
        startpos = transform.position;  //setting player respawn pos 
    }

    private void Start()
    {
        
        Debug.Log("Player respawn set"); 
    }



    // Update is called once per frame
    void Update()
    {
        
        
        //horizontal movement input and only allows jumping when grounded 
        horizontal = Input.GetAxisRaw("Horizontal");

        FuelSlider.value = currentFuelAmount / fuel; 
        if(currentFuelAmount < 0)
        {
            haveFuel = false;
            currentFuelAmount = 0; 
        }
        
        if(Input.GetButtonDown("Jump") && isGrounded())
        {
          
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            animator.SetBool("IsJumping", true);
            spriteRenderer.flipX = true;
            
           
        }
        
        //sets fall when jump button released
        if(Input.GetButtonUp("Jump"))/* && rb.velocity.y > 0)*///need to fix, relying on velocity being greater than zero to speed up fall 
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            animator.SetBool("IsJumping", false);
            spriteRenderer.flipX = false;
            
        }

        if(Input.GetKeyDown(KeyCode.Mouse0) && axeThrown == false) 
        {
            animator.SetTrigger("Attack");
            throwAxe(0);
            axeThrown = true; 

        }
        if(Input.GetKeyUp(KeyCode.J))
        {
            rb.AddForce(-rb.velocity.normalized * jetForce, ForceMode2D.Force);
            isJetPackActive = false;
            animator.SetBool("IsJetPack", false);
            
        }

       if(transform.position.y < -6)
        {
            transform.position = startpos; //snapping player back to start when falls off map 
        }






        Flip();

        //keeps speed at a constant positive number
        animator.SetFloat("Speed", Mathf.Abs(horizontal));
    }

    private void FixedUpdate()
    {
       if(!isJetPackActive && haveFuel)  // 
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
            rb.transform.rotation = Quaternion.Slerp(rb.transform.rotation, targetRot, rotationSpeed * Time.deltaTime); //rotates back towards original rotation when JP not active 
            rb.freezeRotation = true; //stops further rot 
            RefillFuel();

        }
       else if(!haveFuel)
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
            rb.transform.rotation = Quaternion.Slerp(rb.transform.rotation, targetRot, rotationSpeed * Time.deltaTime);
            rb.freezeRotation = true;
            timer += Time.deltaTime;
            if(timer >= refillCoolDown) //punishes player for running out of fuel 
            {
                haveFuel = true;
                timer = 0;
            }
        }
        else
        {
            float invertedRotation = -horizontal * rotationSpeed; //fixed rotation when using jetpack to rotate the appropriate way 
            Vector3 rotation = new Vector3(0, 0, invertedRotation);
            transform.Rotate(rotation);
            rb.freezeRotation = false;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, Time.deltaTime * rotationSpeed).normalized;
            rb.AddForce(transform.rotation * Vector2.up * jetForce);
            currentFuelAmount -= fuelBurnRate * Time.deltaTime; 

            
           
             

        }

        if (Input.GetKey(KeyCode.J) && haveFuel)
        {
            isJetPackActive = true; 
            rb.AddForce(Vector2.up * jetForce,ForceMode2D.Force);
            animator.SetBool("IsJetPack", true);
            rb.gravityScale = 0.8f;
        }

       


    }

    private void RefillFuel()
    {
        if(currentFuelAmount <fuel)
        {
            currentFuelAmount += fuelRefillRate * Time.deltaTime; 
        }
    }

    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        
        
    }

    private void Flip()
    {
        if(isFacingRight && horizontal <0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
    

    public void throwAxe(int value)
    {



      Vector2 ThrowDirection = isFacingRight ? Vector2.right : Vector2.left; //ternary condition checking state of player 
        float rotationZ = isFacingRight ? -20 : 20; 

        GameObject tmp = Instantiate(axePrefab, transform.position, Quaternion.identity);
        AxeThrow axethrowScript = tmp.GetComponent<AxeThrow>(); //fetching throw script to initialise functions 
        axethrowScript.Initialise(ThrowDirection);
        axethrowScript.z = rotationZ;

        axethrowScript.initialPosition = tmp.transform.position;
    }

    public void ResetAxeThrow()
    {
        axeThrown = false;
    }
}
