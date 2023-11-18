using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public PlayerStats playerStats;
    public Animator animator;
    public FireBall fireBall;
    public ArcaneMissile ArcaneMissile;
    public FrostLance frostLance;
    public MageArmor MageArmor;
    public Button AutoAttackButton;
    public Image AutoAttackButtonImage;
    private bool BoolAutoAttackEnabled;
    private float nextAttackTime = 0f;
    private GameObject currentEnemy;
    float maxHealth;
    float currentHealth;
    float maxMana;
    float currentMana;
    float meleeAttackSpeed;

    // Start is called before the first frame update
    void Start()
    {
        initialisePlayer();
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckDetectionRadius(); 
        updateAutoAttackButton();
        
    }

    void initialisePlayer()
    {
        Debug.Log("Player Has " + (playerStats.maxHealth) + " Health");
        maxHealth = playerStats.maxHealth;
       currentHealth = playerStats.maxHealth; 
        maxMana = playerStats.maxMana;
        currentMana = playerStats.maxMana; 
        meleeAttackSpeed = playerStats.meleeAttackSpeed;
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, playerStats.detectionRadius);

    }

    void CheckDetectionRadius()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, playerStats.detectionRadius);

        bool enemyInRange = false; 


        foreach(Collider2D collider in colliders)
        {
            if(collider.CompareTag("Enemy"))
            {
                enemyInRange = true;
                if(collider.gameObject == GameManager.instance.activeEnemy.gameObject)
                {
                    currentEnemy = collider.gameObject;
                    
                }
                else
                {
                    break; ////check to make sure the in range enemy is my target 
                }
                
                
                
                

                AutoAttackButton.interactable = true; AutoAttackButton.enabled = true;
                
                //break;
            }
        }
        nextAttackTime += Time.deltaTime;
        if (enemyInRange && meleeAttackSpeed <= nextAttackTime)
        { 

            if(BoolAutoAttackEnabled) ////doesnt stop if auto attack disabled 
            {
                //animator.SetBool("IsAutoAttacking,", true); 
                //MeleeAttack();
                DamageManager.DealMeleeDamage(currentEnemy, playerStats.baseDamage);
              nextAttackTime = 0;
            }
           else if(!BoolAutoAttackEnabled)
            {
                animator.SetBool("IsAutoAttacking", false);
            }
           
            //AutoAttack.interactable = true; 
           //need a way to un set the auto attack button 
        }
        else if(!enemyInRange)
        {
            //Debug.Log("Not attacking");
            animator.SetBool("IsAutoAttacking",false);
            AutoAttackButtonImage.color = Color.red;
            
            AutoAttackButton.interactable = true; 
            AutoAttackButton.enabled = true;
            currentEnemy = null; 
        }
    }

    void updateAutoAttackButton()
    {
        if (currentEnemy != null && BoolAutoAttackEnabled)
        {
            AutoAttackButtonImage.color = Color.white;
            AutoAttackButton.interactable = true;
            //ToggleAutoAttack();
        }
        else if(currentEnemy != null && !BoolAutoAttackEnabled) 
        {
            AutoAttackButtonImage.color = Color.red;
            AutoAttackButton.interactable = true; 
            //ToggleAutoAttack();
        }
        if(currentEnemy == null && !BoolAutoAttackEnabled)
        {
            
            AutoAttackButtonImage.color = Color.red;
            AutoAttackButton.interactable = true;
        }
        else if (currentEnemy == null && BoolAutoAttackEnabled)
        {
            AutoAttackButtonImage.color = Color.white;
            AutoAttackButton.interactable = true;
        }
    }

    public void ToggleAutoAttack()
    {
        Debug.Log("Pressing Toggle Button");
        
        BoolAutoAttackEnabled = !BoolAutoAttackEnabled;
        if(!BoolAutoAttackEnabled)
        {
            AutoAttackButtonImage.color = Color.red;
            Debug.Log("auto attack disabled");
            currentEnemy = null; 
        }
        else
        {
            AutoAttackButtonImage.color = Color.white;
            Debug.Log("Auto attack enabled"); 

        }
    }


    
    //void MeleeAttack()
    //{
        
       

    //    if(currentEnemy != null)
    //    {
    //        animator.SetBool("IsAutoAttacking", true);
    //        Debug.Log("Melee Attack! with damage of: " + playerStats.baseDamage);
    //        ///damage enemy with base damage 
            
    //    }
    //    else
    //    {
    //        Debug.Log("Cant attack. No current enemy"); 
    //    }
    //}

   public void TakeDamage(float damage)
    {
        damage -= currentHealth;

    }

    

}
