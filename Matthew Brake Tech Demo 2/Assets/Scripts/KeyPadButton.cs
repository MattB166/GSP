using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class KeyPadButton : MonoBehaviour
{
    public TextMeshProUGUI panelText;
    public Image panel;
    public string CorrectCode = "8365";
    public GameObject keyPadPanel;
    public mouseLook mouseLook;
    public Transform door;
    private float panSpeed = 15f;
    public Animator animator;
    public DoorTextTrigger doorTextTrigger;
   


    public void OnNumberButtonClick(string number) // adds number to panel when selected
    {
        panelText.text += number;
        AudioManager.instance.Play("KeyPadPress");
        Debug.Log("adding number");
    }

    public void OnClearButtonClick()     //clears entire string
    {
        panelText.text = string.Empty;
        AudioManager.instance.Play("KeyPadPress");
        panel.color = Color.white;
    }

    public void OnDeleteButtonClick()   //deletes previous number
    {
        AudioManager.instance.Play("KeyPadPress");
        if (panelText.text.Length > 0)
        {
            panelText.text = panelText.text.Substring(0, panelText.text.Length - 1);
        }
        panel.color = Color.white;
    }

    public void OnConfirmButtonClick()    //checks whether number is correct
    {

        AudioManager.instance.Play("KeyPadPress");
        string enteredCode = panelText.text;

        if(enteredCode == CorrectCode)
        {
           
            
                Debug.Log("Correct Code");
                panel.color = Color.green;
                AudioManager.instance.Play("KeyPadGranted");
               mouseLook.PanCameraToTarget(door, panSpeed);
            animator.SetTrigger("OpenDoor");
            AudioManager.instance.Stop("LevelMusic");
            doorTextTrigger.IsKeyPadDone = true;
            //if correct code entered panel lights up green 
            //signals to door that correct code is entered
        }
        else
        {
            panel.color= Color.red;
            AudioManager.instance.Play("KeyPadDenied");
            panelText.text = " ";
            //vice versa if wrong
        }
        
          

            
            
            
    }
}
