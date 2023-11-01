using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class DocumentDisplay : MonoBehaviour
{

    public TextMeshProUGUI documentText;
    public GameObject documentDisplay;
    
    
   public void DisplayDocument(string content)
    {
       if(documentText != null)
        {
            Debug.Log("Content from Scriptable Object: " + content);
            documentDisplay.SetActive(true);
            documentText.text = content;
            gameObject.SetActive(true);
        }
        else
        {
            Debug.LogError("Document text is not assigned in inspector");
        }
       
        
        
       
    }

    public void CloseDocument()
    {
        documentDisplay.SetActive(false);  
        documentText.text = " ";
        gameObject.SetActive(false);
    }
}
