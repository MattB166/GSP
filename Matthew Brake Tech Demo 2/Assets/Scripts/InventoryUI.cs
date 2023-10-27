    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Transform itemSlotParent;
    public GameObject itemSlotPrefab;
    private List<GameObject> itemSlots = new List<GameObject>();

    InventorySO inventory;
   
    
    // Start is called before the first frame update
    void Start()
    {
        inventory = InventorySO.instance;
        inventory.OnItemChangedCallback += UpdateUI;
        
       
    }

   

    void UpdateUI()
    {
        Debug.Log("Updating UI");
    }
}
