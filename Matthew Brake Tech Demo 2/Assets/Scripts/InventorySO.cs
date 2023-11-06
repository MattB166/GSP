using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


//[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/Inventory")]

public class InventorySO : MonoBehaviour
{
    public delegate void OnItemChanged();
    public OnItemChanged OnItemChangedCallback; // checks for new items and removed items 
    
    
    public List<CollectibleItem> collectedItems = new List<CollectibleItem>(); //list of collectibles 
    public static InventorySO instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("More than one instance of inventory");
        }
    }

    public void AddItem(CollectibleItem item) //adds item to collected items list
    {
        collectedItems.Add(item);
        Debug.Log("Item collected: " + item.Name);
        if (OnItemChangedCallback != null)
            OnItemChangedCallback.Invoke();
        
    }

    public void RemoveItem(CollectibleItem item) //removes from list
    {
        collectedItems.Remove(item);
        Debug.Log("Item Removed:" + item.Name);
        if(OnItemChangedCallback != null)
            OnItemChangedCallback.Invoke();
    }

    public bool ContainsKey()   //checking whether a key exists within inventory to allow for outer door to be opened 
    {
        foreach (CollectibleItem item in collectedItems)
        {
            if(item.displayName == "KEY")
            {
                return true;
            }
        }
        return false;
    }
}
