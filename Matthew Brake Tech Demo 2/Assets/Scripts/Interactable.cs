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
    public void Collect()  //sets the function for what happens when an item is collected in game. added to the inventory SO collected list. 
    {
        InventorySO inventory = Object.FindFirstObjectByType<InventorySO>();
        if(inventory != null)
        {
            if(collectible != null)
            {
                inventory.AddItem(collectible);
                Destroy(gameObject);
               
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
    public void OpenDoor() //for the key when the door is accessible 
    {
        Debug.Log("Opening Door");
    }

   
    
    public string Name { get; }
    

    
}
