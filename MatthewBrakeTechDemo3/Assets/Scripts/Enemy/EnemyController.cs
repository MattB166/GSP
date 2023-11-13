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
        CheckforPlayerInRange(); 
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
            Debug.Log("Player in range. Attack Player");
        }
        else
        {
            Debug.Log("Player Out of range");
        }
    }
}
