using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DoorTextTrigger : MonoBehaviour
{
   
    public ToolTipManager ToolTipManager;
    public TextMeshProUGUI tooltipText;
    private bool IsKeyPadDone = false;


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !IsKeyPadDone)
        {
            ToolTipManager.ShowToolTip_Static("I must need to unlock this door!");
        }
    }

}
