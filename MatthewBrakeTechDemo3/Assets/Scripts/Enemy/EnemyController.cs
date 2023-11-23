using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.EnhancedTouch;

public class EnemyController : MonoBehaviour
{
   public EnemyStats EnemyStats;
   public TextMeshProUGUI text; //for testing 
                                ////need reference to UI so can change current target  
                                ///
    
    public enum EnemyState
    {
        Idle, 
        Chase,
        Attack
    }
    public EnemyState enemyState = EnemyState.Idle;
    

    [Header("Stats")]
    public float maxHealth;
    public float currentHealth;
    public float defenceMultiplier;
    public float baseDamage;
    public float aggroSpeed;
    private float AttackTimer = 0f;
    private float timeBetweenAttacks;

    [Header("Enemy References")]
    public Animator enemyAnim; 
    EnemyController currentActiveEnemy;
    public GameObject floatingDamage;
    private Rigidbody2D rb;
    public Vector3 offset = new Vector3(0, 5, 0);
    private Vector3 startingPos;
    private SpriteRenderer sprite;

    private Transform playerTransform = null; 
     
     
    
    // Start is called before the first frame update
    void Start()
    {
        initialiseEnemy(); 
    }

    // Update is called once per frame
    void Update()
    {
        switch(enemyState)
        {
            case EnemyState.Idle:
                break;
            case EnemyState.Chase:
                ChasePlayer();
                break;
            case EnemyState.Attack:
                AttackPlayer();
                break; 

        }
        
        if(Input.touchCount > 0) ///swap to input.get mouse button 
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            { 
                    if(EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                {
                    return;
                }
                
                
                
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Vector2 touchPos = new Vector2(pos.x, pos.y);

                int layermask = LayerMask.GetMask("Enemy", "Ground");

                RaycastHit2D Hit = Physics2D.Raycast(touchPos, Vector2.zero, Mathf.Infinity, layermask);

                Debug.Log("Hit Collider tag: " + Hit.collider.tag);

                   // var Hit = Physics2D.OverlapPoint(touchPos);
                if (Hit.collider != null)
                {
                    if(Hit.collider.CompareTag("Enemy"))
                    {
                        text.GetComponent<TextMeshProUGUI>().text = "Tapped. Target selected";
                        GameManager.instance.SetActiveEnemy(Hit.collider.GetComponent<EnemyController>());
                        currentActiveEnemy = Hit.collider.GetComponent<EnemyController>();
                    }
                    else if(Hit.collider.CompareTag("Ground"))
                    {
                        text.GetComponent<TextMeshProUGUI>().text = "Tapped On ground. Target Deselected";
                        GameManager.instance.SetActiveEnemy(null);
                        currentActiveEnemy = null; 
                    }
                   
                }
                else
                {
                    text.GetComponent<TextMeshProUGUI>().text = "Not tapped. no target selected";
                    // GameManager.instance.SetActiveEnemy(null);
                }
                }
                else if (Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    text.GetComponent<TextMeshProUGUI>().text = "End of tap";
                    
                   

                    
                }
            

        }
        
            


        
        CheckforPlayerInRange(); 
    }

    void initialiseEnemy()
    {
       // Debug.Log("Enemy has " + (EnemyStats.maxHealth) + " Health");
         maxHealth = EnemyStats.maxHealth;
         currentHealth = EnemyStats.currentHealth;
        defenceMultiplier = EnemyStats.defenceMultiplier;
        baseDamage = EnemyStats.baseDamage;
        aggroSpeed = EnemyStats.aggroSpeed;
        timeBetweenAttacks = EnemyStats.rangedAttackSpeed; 
        rb = GetComponent<Rigidbody2D>(); 
        startingPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        sprite = GetComponent<SpriteRenderer>();

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, EnemyStats.detectionRadius);

    }


    void CheckforPlayerInRange()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, EnemyStats.detectionRadius);

        bool PlayerInRange = false;
        //Transform playerTransform = null;
        
        

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                PlayerInRange = true;
                playerTransform = collider.transform;
                enemyState = EnemyState.Chase; 
                    //DamageManager.DealPlayerDamage(collider.gameObject, baseDamage);
                    
             break;
            }
        }
        if (PlayerInRange)
        {
           
             //MoveTowardsPlayer(playerTransform.position);   ////setting aggro 
            // Debug.Log("Player in range. Attack Player");
            //aggro triggered, lock onto player 

        }
        else
        {
           
            //Debug.Log("Player Out of range");
            //playerTransform = null;
            //PlayerInRange = false; 
        }
    }

  public void TakeDamage(float damage)   ///doesnt currently spawn at right position 
  {
        float modifiedDamage = CalculateModifiedDamage(damage);
        currentHealth -= modifiedDamage;
        //Debug.Log(currentHealth);
        DamageManager.ShowDamage((int)modifiedDamage, floatingDamage, transform);        
  }


    public void MoveTowardsPlayer(Vector3 playerPos)  ////need enemy movement animation 
    {
        Vector3 direction = (playerPos - transform.position).normalized;
        enemyAnim.SetFloat("Vertical", playerPos.y - transform.position.y);

        if(Vector3.Distance(playerPos, transform.position) > 1.0f)
        {
            if(playerPos.x > transform.position.x)
            {
                sprite.flipX= true;
            }
            else
            {
                sprite.flipX= false;
            }
            transform.Translate(direction * aggroSpeed * Time.deltaTime);
           
        }
        
        
       
        //rb.velocity = new Vector3(direction.x * aggroSpeed, rb.velocity.y);
        
    }

   
   public float CalculateModifiedDamage(float baseDamage)
    {
        float minDamage = baseDamage * 0.75f;
        float maxDamage = baseDamage * 1.25f;
        float modifiedDamage = Random.Range(minDamage, maxDamage) * defenceMultiplier;
        return modifiedDamage; 
    }

    private void ChasePlayer()
    {
        if(playerTransform != null)
        {
            enemyAnim.SetBool("isChasing", true);
            MoveTowardsPlayer(playerTransform.position);
            
            AttackTimer += Time.deltaTime;
            if(AttackTimer >= timeBetweenAttacks)
            {
                enemyState = EnemyState.Attack;
                AttackTimer = 0f; 
            }
        }
    }
    private void AttackPlayer()
    {
        if(playerTransform != null)
        {
            DamageManager.DealPlayerDamage(playerTransform.gameObject, baseDamage);
            enemyState = EnemyState.Chase;
        }
    }
    


}
