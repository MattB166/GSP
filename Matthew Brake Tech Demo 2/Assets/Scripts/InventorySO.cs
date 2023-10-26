using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/Inventory")]

public class InventorySO : ScriptableObject
{
    public List<CollectibleItem> collectedItems = new List<CollectibleItem>();
}
