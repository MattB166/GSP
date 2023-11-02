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
                    if (interactObj is Interactable interactableScript)
                    {
                        if(interactableScript.objectType != Interactable.ObjectType.KeyPad)
                        {
                            ToolTipManager.ShowToolTip_Static("Press P to Pickup");
                            if (Input.GetKeyDown(KeyCode.P))
                                interactObj.Collect();
                        }
                        else if(interactableScript.objectType == Interactable.ObjectType.KeyPad)
                        {
                            string toolTipText = "Press H to interact with " + interactObj.Name;
                            ToolTipManager.ShowToolTip_Static(toolTipText);
                            if(Input.GetKeyDown(KeyCode.H))
                            
                                interactObj.Interact();
                            
                        }
                        
                    }
                    
                    
                    
                  
                   
                }
            }
            else
            {
                ToolTipManager.HideToolTip_Static();
            }
        }
        
        //{
        //    Ray r = new Ray(InteractorSource.position, InteractorSource.forward);
        //    if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange))
        //    {
        //        if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
        //        {
        //            if(interactObj is IInteractable interactableScript)

        //            interactObj.Interact();
                    
        //        }
        //    }
        //    else
        //    {
        //        ToolTipManager.HideToolTip_Static();
        //    }
        //}
       
    }
}
