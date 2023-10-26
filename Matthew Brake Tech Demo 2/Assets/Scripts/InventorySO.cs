using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/Inventory")]

public class InventorySO : MonoBehaviour
{
    public List<CollectibleItem> collectedItems = new List<CollectibleItem>();


    public void AddItem(CollectibleItem item)
    {
        collectedItems.Add(item);
        Debug.Log("Item collected: " + item.Name);
        
        
    }
}
