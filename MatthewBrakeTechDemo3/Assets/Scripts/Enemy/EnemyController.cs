using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
   public EnemyStats EnemyStats;
    
    
    // Start is called before the first frame update
    void Start()
    {
        initialiseEnemy(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void initialiseEnemy()
    {
        Debug.Log("Enemy has " + (EnemyStats.maxHealth) + " Health");
        float maxHealth = EnemyStats.maxHealth;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, EnemyStats.detectionRadius);

    }


    void CheckforPlayerInRange()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, EnemyStats.detectionRadius);

        bool PlayerInRange = false;

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                PlayerInRange = true;
                break;
            }
        }
        if (PlayerInRange)
        {
            Debug.Log("Enemy in range");
        }
        else
        {
            Debug.Log("Enemy Out of range");
        }
    }
}
