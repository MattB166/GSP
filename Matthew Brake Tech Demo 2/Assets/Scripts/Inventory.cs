using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<Interactable> items = new List<Interactable>();


    public void AddItem(Interactable item)
    {
        items.Add(item);
        item.gameObject.SetActive(false);
        Debug.Log("Item Collected: " + item.objectType.ToString());
        
    }

    public void DropItem(Interactable item)
    {
        items.Remove(item);
        item.gameObject.SetActive(true);
        Debug.Log("Item Dropped: " + item.objectType.ToString());
    }
}
