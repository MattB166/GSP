using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScare : MonoBehaviour
{
    public GameObject jumpScareObj;
    

    
    
    
    // Start is called before the first frame update
    void Start()
    {
        jumpScareObj.SetActive(false);
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            jumpScareObj.SetActive(true);
            
           
            StartCoroutine(DestroyObject());
        }
    }

    IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(jumpScareObj);
        Destroy(gameObject);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
