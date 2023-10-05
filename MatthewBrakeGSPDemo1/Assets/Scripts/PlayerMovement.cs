using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    public float speed = 8f;
    public float jumpingPower = 16f;
    private bool isFacingRight = true;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private GameObject axePrefab; 

    private SpriteRenderer spriteRenderer; //used as jump animation only had one side 
    

    public Animator animator;

    private void Awake()
    {
         spriteRenderer = GetComponent<SpriteRenderer>();
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

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            animator.SetTrigger("Attack");
            throwAxe(0); 
        }

        
        
        Flip();

        //keeps speed at a constant positive number
        animator.SetFloat("Speed", Mathf.Abs(horizontal));
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
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
        if(isFacingRight)
        {
          GameObject tmp = (GameObject) Instantiate(axePrefab, transform.position, Quaternion.identity);
            tmp.GetComponent<AxeThrow>().Initialise(Vector2.right);
            tmp.GetComponent<AxeThrow>().z = -20;
        }
        else
        {
            GameObject tmp = (GameObject)Instantiate(axePrefab, transform.position, Quaternion.identity);
            tmp.GetComponent<AxeThrow>().Initialise(Vector2.left);
            axePrefab.GetComponent<SpriteRenderer>().flipX = true;
            tmp.GetComponent <AxeThrow>().z = 20;

        }
    }
}
