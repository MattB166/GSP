using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickToPlatform : MonoBehaviour
{
    

    private void OnTriggerEnter2D(Collider2D collision) //stops player from falling from moving platform 
    {
        if (collision.gameObject.name == "Ethan Sprite")
        {
            collision.gameObject.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) // releases as a parent 
    {
        if (collision.gameObject.name == "Ethan Sprite")
        {
            collision.gameObject.transform.SetParent(null);
        }
    }
}
