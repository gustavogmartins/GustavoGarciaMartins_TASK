using System.Collections.Generic;
using UnityEngine;

public class GiveItem : MonoBehaviour
{
    public List<Item> Items = new();

    private const int SWORD_ID = 0;
    private const int WOOD_ID = 1;
    private const int FISH_ID = 2;
    private const int CARROT_ID = 3;

    public void GiveSword() {
        InventoryController.Instance.AddItem(Items[SWORD_ID]);
    }

    public void GiveCarrot() {
        InventoryController.Instance.AddItem(Items[CARROT_ID]);
    }

    public void GiveWood() {
        InventoryController.Instance.AddItem(Items[WOOD_ID]);
    }

    public void GiveFish() {
        InventoryController.Instance.AddItem(Items[FISH_ID]);
    }

}
