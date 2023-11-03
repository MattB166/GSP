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
    
    

   public void OnNumberButtonClick(string number)
    {
        panelText.text += number;
        AudioManager.instance.Play("KeyPadPress");
        Debug.Log("adding number");
    }

    public void OnClearButtonClick()
    {
        panelText.text = " ";
        AudioManager.instance.Play("KeyPadPress");
        panel.color = Color.white;
    }

    public void OnDeleteButtonClick()
    {
        AudioManager.instance.Play("KeyPadPress");
        if (panelText.text.Length > 0)
        {
            panelText.text = panelText.text.Substring(0, panelText.text.Length - 1);
        }
        panel.color = Color.white;
    }

    public void OnConfirmButtonClick()
    {

        AudioManager.instance.Play("KeyPadPress");
        string enteredCode = panelText.text;

        if(enteredCode == CorrectCode)
        {
            panel.color = Color.green;
            AudioManager.instance.Play("KeyPadGranted");
           // keyPadPanel.SetActive(false);
        }
        else
        {
            panel.color= Color.red;
            AudioManager.instance.Play("KeyPadDenied");
        }
    }
}
