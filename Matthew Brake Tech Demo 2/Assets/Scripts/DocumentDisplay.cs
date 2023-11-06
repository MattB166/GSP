using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class DocumentDisplay : MonoBehaviour
{

    public TextMeshProUGUI documentText;
    public GameObject documentDisplay;
    private bool isDocumentOpen = false;


    private void Update()
    {
        if(isDocumentOpen && Input.GetKeyDown(KeyCode.Escape)) //closes document on command 
        {
            CloseDocument();
        }
    }


    public void DisplayDocument(string content)  //allows documents to be viewed when pressed on in the inventory 
    {
       if(documentText != null)
        {
            Debug.Log("Content from Scriptable Object: " + content);
            documentDisplay.SetActive(true);
            documentText.text = content;
            gameObject.SetActive(true);
            isDocumentOpen = true; 
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
        isDocumentOpen=false;
    }
}
