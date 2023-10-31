using UnityEngine;
using UnityEngine.UI;


public class inventorySlot : MonoBehaviour
{
    public Image icon; 
    CollectibleItem item;
    public Button removeButton; 

    public void AddItem(CollectibleItem newItem)
    {
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled= true;
        removeButton.interactable= true;
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled= false;   
        removeButton.interactable= false;
    }

    public void OnRemoveButton()
    {
        InventorySO.instance.RemoveItem(item);
        ClearSlot();
        Debug.Log("Removed! Button Clicked");
        
    }
    public void UseItem()
    {
        if(item != null)
        {
            item.Use();
            if(item.displayName == "DOCUMENT")
            {
                item.DisplayItem();
            }
            else
            {
               
                ClearSlot();
            }
        }
    }
}
