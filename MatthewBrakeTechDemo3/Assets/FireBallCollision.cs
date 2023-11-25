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
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Enemy"))
        {
            EnemyController enemy = collision.collider.GetComponent<EnemyController>();

            if(enemy != null)
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
}
