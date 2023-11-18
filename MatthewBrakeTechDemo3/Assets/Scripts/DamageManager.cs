using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TMPro;
using UnityEngine;

public class DamageManager : MonoBehaviour
{
    public static void DealEnemyDamage(GameObject target, float baseDamage)
    {
        EnemyController enemy = target.GetComponent<EnemyController>();

        if(enemy != null)
        {
            float modifiedDamage = enemy.CalculateModifiedDamage(baseDamage);
            enemy.TakeDamage(modifiedDamage);
            Debug.Log("Enemy Taken damge of: " + modifiedDamage);
        }
        else
        {
            Debug.LogError("Target Missing Enemy controller component"); 
        }
    }

    public static void DealPlayerDamage(GameObject target, float baseDamage)
    {
        PlayerController player = target.GetComponent<PlayerController>();

        if(player != null)
        {
            float modifiedDamage = player.CalculateModifiedDamage(baseDamage);
            player.TakeDamage(modifiedDamage);
            Debug.Log("Player Taken damage of: " + modifiedDamage); 
        }
        else
        {
            Debug.LogError("Target Missing Player component"); 
        }
    }

    public static void ShowFloatingDamage(float damage, GameObject floatingDamage, Transform Targettransform, Vector3 offset)
    {
        
        Vector3 spawnPos = Targettransform.position + offset;

        var go = Instantiate(floatingDamage, spawnPos, Quaternion.identity, Targettransform);
        go.GetComponentInChildren<TextMeshProUGUI>().text = damage.ToString();
    }


}
