using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName =  "New Collectible Item", menuName = "Inventory/Collectible Item")]

public class CollectibleItem : ScriptableObject  //scriptable object which determines the type of object in scene it is
{
    
    public string displayName;
    public Sprite icon;
    public GameObject prefab; 

    public string Name
    {
        get { return displayName; }
    }

    public virtual void Use()
    {
        Debug.Log("Using: " + name); 

        
    }
    public virtual void DisplayItem()
    {
        Debug.Log("Item Displaying");
    }
    
}

