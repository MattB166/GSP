using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public PlayerStats playerStats;
    public FireBall fireBall;
    public ArcaneMissile ArcaneMissile;
    public FrostLance frostLance;
    public MageArmor MageArmor;
    public Button AutoAttack;
    private float nextAttackTime = 0f; 
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
                break;
            }
        }
        if(enemyInRange && AutoAttack.interactable && Time.time >= nextAttackTime)
        {
           // Debug.Log("ATTACKING");
            MeleeAttack();
            nextAttackTime = Time.time + 1f / playerStats.meleeAttackSpeed; 
           //need a way to un set the auto attack button 
        }
        else if(AutoAttack.interactable == false)
        {
           Debug.Log("Not attacking");
        }
    }


    
    void MeleeAttack()
    {
        //play animation
        //give base damage to enemy  
        Debug.Log("Melee Attack! with damage of: " + playerStats.baseDamage); 
    }

    

}
