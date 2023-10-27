using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IInteractable
{
   string Name { get; }
    public void Interact();
    public void Collect();
   
}

public class Interactor2 : MonoBehaviour
{
    public Transform InteractorSource;
    public float InteractRange;
    public ToolTipManager ToolTipManager;

    private IInteractable currentInteractable; 
    
    
    // Start is called before the first frame update
    void Start()
    {
        ToolTipManager.HideToolTip_Static();
    }

    // Update is called once per frame
    void Update()
    {
       
        {
            Ray r = new Ray(InteractorSource.position, InteractorSource.forward);
            if(Physics.Raycast(r,out RaycastHit hitInfo, InteractRange))
            {
                if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
                {
                    ToolTipManager.ShowToolTip_Static("Press P to Pickup");  
                    if(Input.GetKeyDown(KeyCode.P))
                    interactObj.Collect();
                  
                   
                }
            }
            else
            {
                ToolTipManager.HideToolTip_Static();
            }
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            Ray r = new Ray(InteractorSource.position, InteractorSource.forward);
            if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange))
            {
                if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
                {
                    interactObj.Interact();
                    string toolTipText = "Press I to interact with " + interactObj.Name;
                    ToolTipManager.ShowToolTip_Static(toolTipText);
                }
            }
            else
            {
                ToolTipManager.HideToolTip_Static();
            }
        }
       
    }
}
