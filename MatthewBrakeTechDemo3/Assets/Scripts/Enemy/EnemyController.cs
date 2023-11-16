using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.EnhancedTouch;

public class EnemyController : MonoBehaviour
{
   public EnemyStats EnemyStats;
   public TextMeshProUGUI text; //for testing 
                                ////need reference to UI so can change current target  

    public float maxHealth;
    public float currentHealth;
    EnemyController currentActiveEnemy;
     
    
    // Start is called before the first frame update
    void Start()
    {
        initialiseEnemy(); 
    }

    // Update is called once per frame
    void Update()
    {
      
        
        if(Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            { 
                    if(EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                {
                    return;
                }
                
                
                
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Vector2 touchPos = new Vector2(pos.x, pos.y);

                int layermask = LayerMask.GetMask("Enemy", "Ground");

                RaycastHit2D Hit = Physics2D.Raycast(touchPos, Vector2.zero, Mathf.Infinity, layermask);

                Debug.Log("Hit Collider tag: " + Hit.collider.tag);

                   // var Hit = Physics2D.OverlapPoint(touchPos);
                if (Hit.collider != null)
                {
                    if(Hit.collider.CompareTag("Enemy"))
                    {
                        text.GetComponent<TextMeshProUGUI>().text = "Tapped. Target selected";
                        GameManager.instance.SetActiveEnemy(Hit.collider.GetComponent<EnemyController>());
                        currentActiveEnemy = Hit.collider.GetComponent<EnemyController>();
                    }
                    else if(Hit.collider.CompareTag("Ground"))
                    {
                        text.GetComponent<TextMeshProUGUI>().text = "Tapped On ground. Target Deselected";
                        GameManager.instance.SetActiveEnemy(null);
                        currentActiveEnemy = null; 
                    }
                   else
                    {
                        //GameManager.instance.SetActiveEnemy(null);
                    }
                }
                else
                {
                    text.GetComponent<TextMeshProUGUI>().text = "Not tapped. no target selected";
                    // GameManager.instance.SetActiveEnemy(null);
                }
                }
                else if (Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    text.GetComponent<TextMeshProUGUI>().text = "End of tap";
                    
                   

                    
                }
            

        }
        
            


        
        CheckforPlayerInRange(); 
    }
    void initialiseEnemy()
    {
       // Debug.Log("Enemy has " + (EnemyStats.maxHealth) + " Health");
         maxHealth = EnemyStats.maxHealth;
         currentHealth = EnemyStats.currentHealth;
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
           // Debug.Log("Player in range. Attack Player");
            //aggro triggered, lock onto player 
        }
        else
        {
            //Debug.Log("Player Out of range");
        }
    }

  void TakeDamage(int damage)
  {

  }
}
