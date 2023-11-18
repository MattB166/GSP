using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageManager : MonoBehaviour
{
    public static void DealEnemyDamage(GameObject target, float damage)
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

    public static void DealPlayerDamage(GameObject target, float damage)
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

    public static void ShowFloatingDamage(float damage, GameObject floatingDamage, Transform Targettransform, Vector3 offset)
    {
        
        Vector3 spawnPos = Targettransform.position + offset;

        var go = Instantiate(floatingDamage, spawnPos, Quaternion.identity, Targettransform);
        go.GetComponentInChildren<TextMeshProUGUI>().text = damage.ToString();
    }


}
