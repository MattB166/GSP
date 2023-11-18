using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManager : MonoBehaviour
{
    public static void DealMeleeDamage(GameObject target, float damage)
    {
        EnemyController enemy = target.GetComponent<EnemyController>();

        if(enemy != null)
        {
            enemy.TakeDamage(damage);
            Debug.Log("Enemy Taken damge of: " + damage);
        }
        else
        {
            Debug.LogError("Target Missing Enemy controller component"); 
        }
    }

    public static void DealPlayerMeleeDamage(GameObject target, float damage)
    {
        PlayerController player = target.GetComponent<PlayerController>();

        if(player != null)
        {
            player.TakeDamage(damage);
            Debug.Log("Player Taken damage of: " + damage); 
        }
        else
        {
            Debug.LogError("Target Missing Player component"); 
        }
    }

   
}
