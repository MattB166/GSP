using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetPack : MonoBehaviour
{

    public float jetSpeed;
    public float maxJetFill;
    public float minJetFill;
    public float jetFill;

    private Rigidbody2D rb;
    public Vector3 DirectionUp = Vector3.up;
    
    // Start is called before the first frame update
    void Start()
    {
       rb = GetComponent<Rigidbody2D>();    
        
        jetSpeed = 100f;
        maxJetFill = 5f;
        minJetFill = 0f;
        jetFill = maxJetFill;

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.J))
        {
            JetPackUp();
        }
        else
        {
            JetPackDown();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(jetFill > 0)
        {
            Vector2 jetForce = Vector2.up* jetFill;
            rb.AddForce(jetForce, ForceMode2D.Force);
            jetFill -= Time.deltaTime;
        }

        //jetFill = Mathf.Clamp(jetFill, 0, maxJetFill);
    }

    public void JetPackUp()
    {
      jetFill -= Time.deltaTime; //losing fuel 

    }

    private void JetPackDown()
    {
        jetFill += Time.deltaTime; //regen fuel 
    }
}
