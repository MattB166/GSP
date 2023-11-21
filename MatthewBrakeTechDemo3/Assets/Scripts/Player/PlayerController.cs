using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public PlayerStats playerStats;
    public Ability fireBall;
    public Ability Armor;
    public Ability Missile;
    public Ability FrostLance; 
    public Animator animator;
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
    float defenceMultiplier;
    public SpriteRenderer playerSpriteColour;
    public GameObject playerDamagePrefab;

    // Start is called before the first frame update
    void Start()
    {
        initialisePlayer();
        Debug.Log("Abilities are: " + playerStats.abilities);
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
        defenceMultiplier = playerStats.defenceMultiplier;
       
        
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

            if(BoolAutoAttackEnabled) //// same damage every time? 
            {
                
                MeleeAttack();
               
               
               
                
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



    void MeleeAttack()
    {



        if (currentEnemy != null)
        {
            animator.SetBool("IsAutoAttacking", true);
            DamageManager.DealEnemyDamage(currentEnemy, playerStats.baseDamage);

        }
        else
        {
            Debug.Log("Cant attack. No current enemy");
        }
    }

    public void TakeDamage(float damage)
    {
        // damage -= currentHealth;
        float modifiedDamage = CalculateModifiedDamage(damage);
        currentHealth -= modifiedDamage;
        DamageManager.ShowDamage((int)modifiedDamage, playerDamagePrefab, transform);
    }

    private IEnumerator isCasting(float castTime) ///for particle effects? if poss. might not work as only produces a delay 
    {
        yield return new WaitForSeconds(castTime);
        ///cast particles?  maybe make this a bool and send it to game manager instead 
    }

    public float CalculateModifiedDamage(float baseDamage)
    {
        float minDamage = baseDamage * 0.75f;
        float maxDamage = baseDamage * 1.25f;
        float modifiedDamage = Random.Range(minDamage,maxDamage) * defenceMultiplier;
        return modifiedDamage;
    }

    public void FireBall(Transform target)  //spawn fireball from trans.pos. vector is distance between player and enemy 
    {
        StartCoroutine(isCasting(fireBall.castingTime));
        StartCoroutine(GameManager.instance.LoadCastBar(fireBall.castingTime));
        Vector3 distance = new Vector3(transform.position.x - target.position.x, transform.position.y - target.position.y); ///calculate vector between players 
        ///spawn fireball prefab at player transform and have it transform towards enemy until hits its collider 
        /// var go = Instantiate(FireBallPrefab, transform.position, quaternion.rotation); 
        /// go.rb.addVelocity("towards current enemy transform. 
        
        Debug.Log("FireBall Produced");
       
    
          
    
    }

    

}
