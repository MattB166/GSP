using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TMPro;
using UnityEngine;

public class DamageManager : MonoBehaviour
{
    public static DamageManager instance;
    
    public static float hitChance = 80;

    private void Awake()
    {
        if (instance == null)
        {
            {
                instance = this;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void DealEnemyDamage(EnemyController target, float baseDamage) ///this bool might not be best idea as multiple abilities need incoporating 
    {
        if (IsHit(hitChance))
        {
            bool isCritical = IsCriticalHit();
            EnemyController enemy = target.GetComponent<EnemyController>();

            if (enemy != null)
            {
                if(isCritical)
                {
                    baseDamage *= 2;
                    
                }
                else
                {
                  
                    float modifiedDamage = enemy.CalculateModifiedDamage(baseDamage);
                    enemy.TakeDamage(modifiedDamage);
                    
                   
                }
               
            }
            else
            {
                Debug.LogError("Target Missing Enemy controller component");
            }

        }
        else
        {
            Debug.Log("Attack Missed");
        }
    }

    public static void DealPlayerDamage(GameObject target, float baseDamage)
    {
       if(IsHit(hitChance))
        {
            bool isCritical = IsCriticalHit();
            PlayerController player = target.GetComponent<PlayerController>();
            if (player != null)
            {
                if(isCritical)
                {
                    baseDamage *= 2;
                }
                else
                {
                    float modifiedDamage = player.CalculateModifiedDamage(baseDamage);
                    player.TakeDamage(modifiedDamage);
                    //Debug.Log("Player Taken damage of: " + modifiedDamage); 
                }

            }
            else
            {
                Debug.LogError("Target Missing Player component");
            }
        }
        

        
    }

    public static void ShowDamage(int damage, GameObject damagePrefab, Transform transform)
    {
        Vector3 offset = new Vector3(0, 2, 0); 
        var go = Instantiate(damagePrefab, transform.position + offset, Quaternion.identity, transform);
       // Debug.Log("Instantiating Damage prefab: " + " pos: " + transform.position + offset);
        go.GetComponent<TextMesh>().text = damage.ToString();
        Destroy(go, 1f); 
    }

    public static bool IsHit(float hitChance)
    {
        float randomValue = Random.value * 100;

        return randomValue <= hitChance; 
    }
    private static bool IsCriticalHit()
    {
        float criticalChance = 20;
        float randomCritical = Random.value * 100;
        //Debug.Log("Random Critical Value: " + randomCritical);

       if(randomCritical <= criticalChance)
        {
            return true;
        }
        else
        {
            return false; 
        }    
            
    }
    private static bool IsMissileCrit()
    {
        float criticalChance = 25;
        float randomCritical = Random.value * 100;
        if(randomCritical <= criticalChance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    public static void DealEnemyAbilityDamage(EnemyController target, Ability ability)
    {
        //Debug.Log("Entered ability damage function");
        if (IsHit(hitChance))
        {
            Debug.Log("Ability has hit");
            bool isCrit = IsCriticalHit();
            bool isMissileCrit = IsMissileCrit();   

            switch(ability)
            {
                case FireBall fireBall:
                    //Debug.Log("Ability is fireball"); 
                    DealFireBallDamage(target, fireBall, isCrit);
                    //Debug.Log("Doing Damage");
                    break;
                case ArcaneMissile arcaneMissile:
                    DealArcaneMissileDamage(target,arcaneMissile,isMissileCrit);
                    break;

            }
        }
        else
        {
            Debug.Log("Missed enemy with ability");
        }
    }

    private static void DealFireBallDamage(EnemyController target, FireBall fireBall, bool isCrit)
    {
        target.TakeDamage(fireBall.basePower);
        //Debug.Log("Dealt initial Damage of Fireball"); 

        //now for DOT 
        float additionalDamage = fireBall.additionalDamage;
        float interval = fireBall.additionalDamageInterval;
        
        
       //float DOT = calculateDamageOverTime(fireBall.additionalDamage, fireBall.additionalDamageInterval, fireBall.debuffDuration);
        if(isCrit == true)
        {
            Debug.Log("Critical. DOT doubled"); 
            additionalDamage *= 2; 
        }

        int numSecs = Mathf.CeilToInt(fireBall.debuffDuration / fireBall.additionalDamageInterval);

      instance.StartCoroutine(ApplyDamageOverTime(target,additionalDamage,interval, numSecs));
    }
    private static void DealArcaneMissileDamage(EnemyController target, ArcaneMissile arcaneMissile, bool isMissileCrit)
    {
        if(isMissileCrit)
        {
            //moment of brilliance
        }
        else
        {
            target.TakeDamage(arcaneMissile.basePowerPerMissile); 
        }
    }

    private static float calculateDamageOverTime(float additionalDamage, float additionalDamageInterval, float duration)
    {
        return (duration / additionalDamageInterval) * additionalDamage;
    }

    private static IEnumerator ApplyDamageOverTime(EnemyController target, float damage, float interval, int secs)
    {
        Debug.Log("Dealing Damage Over time");
        for (int i = 0; i < secs; i++)
        {
            yield return new WaitForSeconds(interval);
            target.TakeDamage(damage);
            
        }
    }
   


}
