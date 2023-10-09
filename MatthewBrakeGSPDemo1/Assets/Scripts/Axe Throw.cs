using System.Collections;
using System.Collections.Generic;
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
    private Vector2 initialPosition;
    public float maxDistance = 10f;

    private Vector2 direction;

    private bool isReturning = false;
    
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        initialPosition = transform.position;
        
    }

    private void FixedUpdate()
    {
       if(!isReturning)
        {
            rb.velocity = direction * speed;
        }
        else
        {
            rb.velocity = -direction * speed;
        }
        
        
        
        rb.velocity = direction * speed;
        currentEulerAngles += new Vector3(0,0,z) * Time.deltaTime * rotationSpeed;
        transform.localEulerAngles = currentEulerAngles; 

        float currentDistance = Vector2.Distance(initialPosition, transform.position);

        if(currentDistance >= maxDistance)
        {
            direction = -direction;

            isReturning = !isReturning;
        }
       
    }

    public void Initialise(Vector2 direction)
    {
        this.direction = direction;
    }



    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
