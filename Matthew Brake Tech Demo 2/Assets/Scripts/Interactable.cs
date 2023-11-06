using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

public class Interactable : MonoBehaviour, IInteractable

{

    public UnityEvent onInteract;
    public int ID;
    public Sprite interactIcon;
    public Vector2 iconSize;
    [SerializeField]
    private CollectibleItem collectible; 
    
    public enum ObjectType
    {
        Key,
        Document,
        Hint,
        Weapon,
        KeyPad,
        Door 

    }

    [SerializeField]
    public ObjectType objectType;

    public void Interact()
    {
        Debug.Log("Interacted");
    }
    public void Collect()
    {
        InventorySO inventory = Object.FindFirstObjectByType<InventorySO>();
        if(inventory != null)
        {
            if(collectible != null)
            {
                inventory.AddItem(collectible);
                Destroy(gameObject);
               // Debug.Log("Item Collected: " + collectible.Name);
            }
            else
            {
                Debug.Log("Collectible Item not assigned to this interactable");
            }
        }
        else
        {
            Debug.Log("Inventory not found"); 
        }
    }
    public void OpenDoor()
    {
        Debug.Log("Opening Door");
    }

    //public void Drop()
    //{
    //    InventorySO inventory = Object.FindFirstObjectByType<InventorySO>();
    //    if(inventory != null)
    //    {
    //        inventory.DropItem(this);
    //    }
    //    else
    //    {
    //        Debug.Log("Warning. Inventory not found");
    //    }
    //}
    
    public string Name { get; }
    

    // Start is called before the first frame update
    void Start()
    {
       // ID = Random.Range(0, 999999);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
