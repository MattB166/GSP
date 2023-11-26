using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcaneMissileCollision : MonoBehaviour
{
    public ArcaneMissile ArcaneMissile;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            EnemyController enemy = collision.GetComponent<EnemyController>();
            if(enemy != null)
            {
                DamageManager.DealEnemyAbilityDamage(enemy, ArcaneMissile);
            }
            else
            {
                Debug.Log("Enemy Null");
            }
            Destroy(gameObject); 
        }
    }
}
