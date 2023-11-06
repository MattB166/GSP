using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Document", menuName = "Inventory/Document Item")]
public class DocumentItem : CollectibleItem
{
    [TextArea(3,10)]
    public string content;
}
