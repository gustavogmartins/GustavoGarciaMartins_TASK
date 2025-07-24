using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{
    public string ItemName;
    public Sprite Icon;
    public bool IsStackable;
    public int MaxStack = 3;
    public bool IsUsable;
    public float RecoverAmout = 0;
}
