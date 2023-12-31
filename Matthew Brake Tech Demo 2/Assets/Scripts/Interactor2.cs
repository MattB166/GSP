using JetBrains.Annotations;
//using Mono.Cecil.Cil;
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
    public InventorySO InventorySO;
    public GameObject Keypad;
    public Material material;
    private Renderer objectRenderer;
    private Renderer storedRenderer;
    public Material originalKeyMat;
    public Material originalDocMat;
    public Material originalKeyPadMat;
    public Material originalDoorMat;

    private IInteractable currentInteractable; 
    
    
    // Start is called before the first frame update
    void Start()
    {
        ToolTipManager.HideToolTip_Static();  //hides initial tool tips and sets keypad to false 
        
        Keypad.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
       
        {
            Ray r = new Ray(InteractorSource.position, InteractorSource.forward);
            
            if(Physics.Raycast(r,out RaycastHit hitInfo, InteractRange))
            {
                if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))   //detects objects with interactable script attached to them
                {
                   storedRenderer = objectRenderer;
                    currentInteractable = interactObj;
                    if (interactObj is Interactable interactableScript)
                    {

                        if (interactableScript.objectType == Interactable.ObjectType.Key || interactableScript.objectType == Interactable.ObjectType.Document)
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
                            string toolTipText = "Press H to Use " + interactObj.Name;
                            ToolTipManager.ShowToolTip_Static(toolTipText);
                            
                            if(Input.GetKeyDown(KeyCode.H))
                            {
                                AudioManager.instance.Stop("LevelMusic");
                                AudioManager.instance.Play("Pressure");
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
                        else if (interactableScript.objectType == Interactable.ObjectType.Door)
                        {
                            if(InventorySO.ContainsKey())
                            {
                                string toolTipText = "Use the key to open the door!";
                                ToolTipManager.ShowToolTip_Static(toolTipText);
                               
                                if (Input.GetKeyDown(KeyCode.I))
                                {
                                    ToolTipManager.HideToolTip_Static();
                                }
                                   
                            }
                            else
                            {
                                ToolTipManager.ShowToolTip_Static("You need a key to open the door!");
                            }
                            

                        }
                      

                    }  objectRenderer = hitInfo.collider.GetComponent<MeshRenderer>();   //sets the highlight material on these objects 
                        objectRenderer.material = material;

                    



                }
               
            }
            else
            {
                ToolTipManager.HideToolTip_Static();

                RestoreOriginalMat(storedRenderer); //restores original materials in function below 
                   
               
                
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
                    case Interactable.ObjectType.Door:
                        Debug.Log("Restoring Door Mat");
                        renderer.material = originalDoorMat;
                        break;
                        

                }
            }
        }
    }
}
