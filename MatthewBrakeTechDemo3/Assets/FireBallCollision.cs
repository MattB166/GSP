using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallCollision : MonoBehaviour
{
    public FireBall fireBall;


    //public void setFireBall(FireBall fb)
    //{
    //    fireBall = fb;
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyController enemy = collision.GetComponent<EnemyController>();

            if (enemy != null)
            {
                DamageManager.DealEnemyAbilityDamage(enemy, fireBall);
                Debug.Log("Damage done to enemy through fireball");
            }
            else
            {
                Debug.Log("enemy null");
            }
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
