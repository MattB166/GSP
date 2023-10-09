using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]  
public class AxeThrow : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private float rotationSpeed = 80;
    Vector3 currentEulerAngles;
    public float z;
    private Rigidbody2D rb;
    public Vector2 initialPosition;
    public float maxDistance = 1f;

    private Vector2 direction;

    private bool isReturning = false;
    float distanceToPlayer;
    public float returnDistanceDestroy = 0.1f;

    private Transform playerTransform;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        initialPosition = transform.position;

        playerTransform = GameObject.FindGameObjectWithTag("Ethan Sprite").transform;
        
        
    }

    private void FixedUpdate()
    {
       if(!isReturning)
        {
            rb.velocity = direction.normalized * speed;
           // Debug.Log("Axe Thrown");
        }
        else
        {
           
            Vector2 returnDirection = ((Vector2)playerTransform.position - (Vector2)transform.position).normalized;
            rb.velocity = returnDirection * speed; 
            //Debug.Log("axe returning");

            distanceToPlayer = Vector2.Distance(playerTransform.position, transform.position);

            if(distanceToPlayer < returnDistanceDestroy)
            {
                DestroyAxe();
                Debug.Log("Axe Destroyed");
            }
           
        }
        
        
        
        //rb.velocity = direction * speed;
        currentEulerAngles += new Vector3(0,0,z) * Time.deltaTime * rotationSpeed;
        transform.localEulerAngles = currentEulerAngles; 

        float currentDistance = Vector2.Distance(initialPosition, transform.position);

        if(currentDistance >= maxDistance)
        {
            direction = -direction;

            isReturning = true;
        }

       
    }

    public void Initialise(Vector2 direction)
    {
        this.direction = direction;
    }

    private void DestroyAxe()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnBecameInvisible()
    {
        DestroyAxe();
    }
}
