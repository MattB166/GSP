using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Interactor : MonoBehaviour
{

    public LayerMask interactableLayerMask = 7;
    Interactable interactable;
    public Image interactImage; 
    public Sprite defaultIcon;
    public Sprite defaultInteractIcon;
    public Vector2 defaultIconSize;
    public Vector2 defaultInteractIconSize; 
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 4, interactableLayerMask))
        {
            if (hit.collider.GetComponent<Interactable>() != false)
            {
                if (interactable == null || interactable.ID != hit.collider.GetComponent<Interactable>().ID)
                {
                    interactable = hit.collider.GetComponent<Interactable>();

                }
               
                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactable.onInteract.Invoke();
                }
            }
        }
       
    }
}
