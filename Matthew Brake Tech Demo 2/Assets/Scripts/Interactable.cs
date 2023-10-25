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
        gameObject.SetActive(false);
    }
    public void Collect()
    {
        gameObject.SetActive(false);
        Debug.Log("Item collected");
    }

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
