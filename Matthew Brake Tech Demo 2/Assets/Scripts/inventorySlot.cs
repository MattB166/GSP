using UnityEngine;
using UnityEngine.UI;


public class inventorySlot : MonoBehaviour
{
    public Image icon; 
    CollectibleItem item;
    public Button removeButton; 
    public DocumentDisplay display;
    public Animator OutDoorAnimator;
    public ToolTipManager toolTipManager;
    
    

    public void AddItem(CollectibleItem newItem)  //adds the already collected item to a slot in the inventory. this is called from the inventory UI script
    {
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled= true;
        removeButton.interactable= true;
    }

    public void ClearSlot()  // removes from the UI slot. 
    {
        item = null;
        icon.sprite = null;
        icon.enabled= false;   
        removeButton.interactable= false;
    }

    public void OnRemoveButton()  // deletes from the inventory and from the UI, unless it is a key as it is an important object of use. 
    {
        if(item.displayName != "KEY")
        {
            InventorySO.instance.RemoveItem(item);
            ClearSlot();
            Debug.Log("Removed! Button Clicked");
        }
        else
        {
           
           // Debug.Log("Cant delete important object!");
            ToolTipManager.ShowToolTip_Static("Can't delete important items!");
            
        }
        
    }
    public void UseItem()  // this determines how the object is used. if it is a key it opens the door, and if it is a document it is viewed in the inventory. 
    {
        if(item != null)
        {
            item.Use();
            if(item.displayName == "KEY")
            {
                Debug.Log("Door Opening");
                OutDoorAnimator.SetTrigger("IsOuterDoorUnlocked");
                //ToolTipManager.ShowToolTip_Static("Freedom!");
            }
            if(item.displayName == "DOCUMENT" && item is DocumentItem documentItem)
            {
                Debug.Log("carrying over document content to Doc Display: " + documentItem.content);
                display.DisplayDocument(documentItem.content);
            }
            else
            {
               
                ClearSlot();
            }
        }
    }

    //public void DisplayDocument(string documentContent)
    //{
        
    //}
}
