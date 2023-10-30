    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Transform itemParent;
   // public GameObject itemSlotPrefab;
   // private List<GameObject> itemSlots = new List<GameObject>();

    InventorySO inventory;


    [SerializeField]
    private inventorySlot[] slots;
   
    
    // Start is called before the first frame update
    void Start()
    {
        inventory = InventorySO.instance;
        inventory.OnItemChangedCallback += UpdateUI;

       slots = itemParent.GetComponentsInChildren<inventorySlot>();
        
       
    }



    void UpdateUI()
    {

        if (inventory == null)
        {
            Debug.LogError("Inventory is not assigned in the Inspector.");
            return;
        }
        if (slots == null)
        {
            Debug.LogWarning("Inventory slots array is null.");
            return; // Exit the method to prevent further errors
        }


        if (itemParent == null)
        {
            Debug.LogError("itemParent is not assigned in the Inspector.");
            return;
        }

        if (slots == null || slots.Length == 0)
        {
            Debug.LogError("No inventory slots (inventorySlot) are assigned.");
            return;
        }


        if (slots != null)
        {

            
           
            
            for (int i = 0; i < slots.Length && i < inventory.collectedItems.Count; i++)
            {
                if (i < inventory.collectedItems.Count)
                {
                    
                    slots[i].AddItem(inventory.collectedItems[i]);
                    Debug.Log("Collected items: ");
                    print(inventory.collectedItems);
                    Debug.Log("length of slots");
                    print(slots.Length);
                    Debug.Log("Item added to inventory");
                }
                else
                {
                    slots[i].ClearSlot();
                }
            }

           
        }
        else
        {
            Debug.LogWarning("Inventory slots array is not defined");
        }
    }
}
