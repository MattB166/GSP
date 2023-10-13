using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static Unity.Burst.Intrinsics.X86.Avx;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    public float speed = 8f;
    public float rotationSpeed = 5f; 
    public float jumpingPower = 16f;
    private bool isFacingRight = true;
    public bool axeThrown = false;
    private float jetForce = 15f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private GameObject axePrefab; 
    [SerializeField] private bool isJetPackActive = false;
    private Quaternion targetRot = Quaternion.identity;

    private SpriteRenderer spriteRenderer; //used as jump animation only had one side 
    

    public Animator animator;

    private void Awake()
    {
         spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        
    }



    // Update is called once per frame
    void Update()
    {
        
        
        //horizontal movement input and only allows jumping when grounded 
        horizontal = Input.GetAxisRaw("Horizontal");
        
        
        
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
        
        


        
        
        Flip();

        //keeps speed at a constant positive number
        animator.SetFloat("Speed", Mathf.Abs(horizontal));
    }

    private void FixedUpdate()
    {
       if(!isJetPackActive)
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
            rb.transform.rotation = Quaternion.Slerp(rb.transform.rotation, targetRot, rotationSpeed * Time.deltaTime);
            rb.freezeRotation = true;

        }
        else
        {
            //// rb.velocity = new Vector2(horizontal/2 * speed,rb.velocity.y);
            float invertedRotation = -horizontal * rotationSpeed;
            Vector3 rotation = new Vector3(0, 0, invertedRotation);
            transform.Rotate(rotation);
            rb.freezeRotation = false;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, Time.deltaTime * rotationSpeed).normalized;
            rb.AddForce(transform.rotation * Vector2.up * jetForce);
            //rb.AddTorque(horizontal * rotationSpeed, ForceMode2D.Force);


            
           
             

        }
       
        if(Input.GetKey(KeyCode.J))
        {
            isJetPackActive = true; 
            rb.AddForce(Vector2.up * jetForce,ForceMode2D.Force);
            animator.SetBool("IsJetPack", true);
            rb.gravityScale = 0.8f;
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
        //if (isFacingRight)
        //{
        //    GameObject tmp = (GameObject)Instantiate(axePrefab, transform.position, Quaternion.identity);
        //    tmp.GetComponent<AxeThrow>().Initialise(Vector2.right);
        //    tmp.GetComponent<AxeThrow>().z = -20;
        //}
        //else
        //{
        //    GameObject tmp = (GameObject)Instantiate(axePrefab, transform.position, Quaternion.identity);
        //    tmp.GetComponent<AxeThrow>().Initialise(Vector2.left);
        //    axePrefab.GetComponent<SpriteRenderer>().flipX = true;
        //    tmp.GetComponent<AxeThrow>().z = 20;

        //}



        Vector2 ThrowDirection = isFacingRight ? Vector2.right : Vector2.left;
        float rotationZ = isFacingRight ? -20 : 20;

        GameObject tmp = Instantiate(axePrefab, transform.position, Quaternion.identity);
        AxeThrow axethrowScript = tmp.GetComponent<AxeThrow>();
        axethrowScript.Initialise(ThrowDirection);
        axethrowScript.z = rotationZ;

        axethrowScript.initialPosition = tmp.transform.position;
    }

    public void ResetAxeThrow()
    {
        axeThrown = false;
    }
}
