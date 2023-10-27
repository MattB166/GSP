using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/Inventory")]

public class InventorySO : MonoBehaviour
{
    public delegate void OnItemChanged();
    public OnItemChanged OnItemChangedCallback;
    
    
    public List<CollectibleItem> collectedItems = new List<CollectibleItem>();
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

    public void AddItem(CollectibleItem item)
    {
        collectedItems.Add(item);
        Debug.Log("Item collected: " + item.Name);
        if (OnItemChangedCallback != null)
            OnItemChangedCallback.Invoke();
        
    }
}
