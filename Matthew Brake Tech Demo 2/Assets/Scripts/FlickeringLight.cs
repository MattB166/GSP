using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    public Light _light;
    public float MinTime;
    public float MaxTime;
    public float timer;

   

    void Start()
    {
        timer = Random.Range(MinTime, MaxTime); 
    }
 

    void Update()
    {
        FlickerLight();
    }

    void FlickerLight()
    {
        if(timer > 0)
          timer -= Time.deltaTime;
        
        if(timer <= 0)
        {
            _light.enabled = !_light.enabled;
            timer = Random.Range(MinTime, MaxTime);
        }

        
    }
}
