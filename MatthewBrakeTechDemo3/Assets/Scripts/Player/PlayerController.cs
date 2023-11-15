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
    public Button AutoAttack;
    public Image AutoAttackButtonImage;
    private bool AutoAttackEnabled = false;
    private float nextAttackTime = 0f;
    private GameObject currentEnemy;
    float maxHealth;
    float currentHealth;
    float maxMana;
    float currentMana;

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
                currentEnemy = collider.gameObject;
                AutoAttack.interactable = true;
                break;
            }
        }
        if(enemyInRange && AutoAttack.interactable && Time.time >= nextAttackTime)
        {
           // Debug.Log("ATTACKING");
            MeleeAttack();
            nextAttackTime = Time.time + 1f / playerStats.meleeAttackSpeed;
            //AutoAttack.interactable = true; 
           //need a way to un set the auto attack button 
        }
        else if(!enemyInRange || AutoAttack.interactable == false)
        {
           Debug.Log("Not attacking");
            AutoAttackButtonImage.color = Color.red;
            AutoAttack.interactable = false; 
            currentEnemy = null; 
        }
    }

    void updateAutoAttackButton()
    {
        if (currentEnemy != null)
        {
            AutoAttackButtonImage.color = Color.white;
        }
        else
        {
            AutoAttackButtonImage.color = Color.red;
            AutoAttack.interactable = false; 
        }
    }

    public void ToggleAutoAttack()
    {
        AutoAttackEnabled = !AutoAttackEnabled;
        if(!AutoAttackEnabled)
        {
            AutoAttackButtonImage.color = Color.red;
            currentEnemy = null; 
        }
        else
        {
            AutoAttackButtonImage.color = Color.white;

        }
    }


    
    void MeleeAttack()
    {
        //play animation
        //give base damage to enemy  
        animator.SetBool("IsAutoAttacking", true);
        Debug.Log("Melee Attack! with damage of: " + playerStats.baseDamage); 

        if(currentEnemy != null)
        {

            ///damage enemy with base damage 

        }
    }

    

}
