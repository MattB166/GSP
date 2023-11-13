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
}
