using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TMPro;
using UnityEngine;

public class DamageManager : MonoBehaviour
{
    public static float hitChance = 80;
   
    
    public static void DealEnemyDamage(EnemyController target, float baseDamage, bool applyDOT) ///this bool might not be best idea as multiple abilities need incoporating 
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
                    if(applyDOT)
                    {
                        //apply DOT. and if critical, the additional damage is doubled. need a DOT function below 
                    }
                   
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


    public static void DealEnemyAbilityDamage(EnemyController target, Ability ability)
    {
        Debug.Log("Entered ability damage function");
        if (IsHit(hitChance))
        {
            bool isCrit = IsCriticalHit();


            switch(ability)
            {
                case FireBall fireBall:
                    Debug.Log("Ability is fireball"); 
                    DealFireBallDamage(target, fireBall, isCrit);
                    Debug.Log("Doing Damage");
                    break;
                case ArcaneMissile arcaneMissile:
                    DealArcaneMissileDamage(target,arcaneMissile,isCrit);
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

        //now for DOT 
        
       float DOT = calculateDamageOverTime(fireBall.additionalDamage, fireBall.additionalDamageInterval, fireBall.debuffDuration);
        if(isCrit)
        {
            DOT *= 2; 
        }

        int numSecs = Mathf.CeilToInt(fireBall.debuffDuration / fireBall.additionalDamageInterval);

        DamageManager.ApplyDamageOverTime(target,DOT,fireBall.additionalDamageInterval,numSecs);
    }
    private static void DealArcaneMissileDamage(EnemyController target, ArcaneMissile arcaneMissile, bool isCrit)
    {

    }

    private static float calculateDamageOverTime(float additionalDamage, float additionalDamageInterval, float duration)
    {
        return (duration / additionalDamageInterval) * additionalDamage;
    }

    private static IEnumerator ApplyDamageOverTime(EnemyController target, float damage, float interval, int secs)
    {
        for(int i = 0; i < secs; i++)
        {
            yield return new WaitForSeconds(interval);
            target.TakeDamage(damage);
        }
    }
   


}
