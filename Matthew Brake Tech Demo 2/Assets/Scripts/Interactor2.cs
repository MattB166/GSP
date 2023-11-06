using JetBrains.Annotations;
using Mono.Cecil.Cil;
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
    public GameObject Keypad;
    public Material material;
    private Renderer objectRenderer;
    private Renderer storedRenderer;
    public Material originalKeyMat;
    public Material originalDocMat;
    public Material originalKeyPadMat;

    private IInteractable currentInteractable; 
    
    
    // Start is called before the first frame update
    void Start()
    {
        ToolTipManager.HideToolTip_Static();
        
        Keypad.SetActive(false);
        
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
                   storedRenderer = objectRenderer;
                    currentInteractable = interactObj;
                    if (interactObj is Interactable interactableScript)
                    {
                        
                        if(interactableScript.objectType != Interactable.ObjectType.KeyPad)
                        {
                            ToolTipManager.ShowToolTip_Static("Press P to Pickup");
                            if (Input.GetKeyDown(KeyCode.P))
                            {
                                interactObj.Collect();
                                ToolTipManager.HideToolTip_Static();
                            }
                               
                        }
                        else if(interactableScript.objectType == Interactable.ObjectType.KeyPad)
                        {
                            string toolTipText = "Press H to interact with " + interactObj.Name;
                            ToolTipManager.ShowToolTip_Static(toolTipText);
                            if(Input.GetKeyDown(KeyCode.H))
                            {
                                interactObj.Interact();
                                ToolTipManager.HideToolTip_Static();
                                if(Keypad != null)
                                {
                                    if (Keypad.activeSelf)
                                    {
                                        Keypad.SetActive(false);
                                        Time.timeScale = 1.0f;

                                    }
                                    else
                                    {
                                        Keypad.SetActive(true);
                                        Time.timeScale = 0f; 
                                     
                                    }
                                }
                            }
                                
                            
                        }
                      

                    }  objectRenderer = hitInfo.collider.GetComponent<MeshRenderer>();
                        objectRenderer.material = material;

                    



                }
               
            }
            else
            {
                ToolTipManager.HideToolTip_Static();

                RestoreOriginalMat(storedRenderer);
                   
               
                
            }
        }
       
        
      
    }
   
    private void RestoreOriginalMat(Renderer renderer)
    {
        if(renderer != null)
        {
            if(currentInteractable is Interactable interactable)
            {
                switch(interactable.objectType)
                {
                    case Interactable.ObjectType.Document:
                        Debug.Log("Restoring Document Material");
                        renderer.material = originalDocMat;
                        break;
                    case Interactable.ObjectType.Key:
                        Debug.Log("Restoring Key Mat");
                        renderer.material = originalKeyMat;
                        break;
                    case Interactable.ObjectType.KeyPad:
                        Debug.Log("Restoring KeyPad Mat");
                        renderer.material = originalKeyPadMat;
                        break;
                        

                }
            }
        }
    }
}
