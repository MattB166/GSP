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

    
    public enum ObjectType
    {
        Key,
        Document,
        Hint,
        Weapon

    }

    [SerializeField]
    public ObjectType objectType;

    public void Interact()
    {
        Debug.Log("Interacted");
    }
    public void Collect()
    {
        //gameObject.SetActive(false);
        Inventory inventory = Object.FindFirstObjectByType<Inventory>();
        if (inventory != null)
        {
            inventory.AddItem(this);
           // Debug.Log("Item collected");
        }
        else
        {
            Debug.Log("Warning. Inventory not found");
        }
        gameObject.SetActive(false);
    }

    public void Drop()
    {
        Inventory inventory = Object.FindFirstObjectByType<Inventory>();
        if(inventory != null)
        {
            inventory.DropItem(this);
        }
        else
        {
            Debug.Log("Warning. Inventory not found");
        }
    }
    
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
