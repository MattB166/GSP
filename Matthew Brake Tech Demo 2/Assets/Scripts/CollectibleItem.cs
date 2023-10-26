using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName =  "New Collectible Item", menuName = "Inventory/Collectible Item")]
public class CollectibleItem : ScriptableObject
{
    public string id;
    public string displayName;
    public Sprite icon;
    public GameObject prefab; 
}
