using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderMovement : MonoBehaviour
{

    private float vertical;
    private float speed = 10f;
    private bool isLadder;
    private bool isClimbing;
    public Animator animator;

    [SerializeField] private Rigidbody2D rb; 
    
   

    // Update is called once per frame
    void Update()
    {
        vertical = Input.GetAxis("Vertical");
        if (isLadder && Mathf.Abs(vertical) > 0 || isLadder &&  Mathf.Abs(vertical) < 1) //if player is by ladder and up or down is being pressed 
        {
            isClimbing = true;
            Debug.Log("Climbing!");
            animator.SetBool("IsClimbing",true);
        }
        else
        {
            //isClimbing = false;
            animator.SetBool("IsClimbing", false);
        }
    }

    private void FixedUpdate()
    {
        if (isClimbing == true)
        {
            rb.gravityScale = 0;
            rb.velocity = new Vector2(rb.velocity.x, vertical * speed); //allows player to climb without any gravity acting against 
        }
        else
        {
            rb.gravityScale = 5f;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Ethan Sprite"))
        {
            isLadder = true;
            Debug.Log("Entered Ladder Trigger");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isLadder = false;
        isClimbing = false;
        animator.SetBool("IsClimbing", false);
        Debug.Log("Exited ladder trigger"); 
    }

    
}
