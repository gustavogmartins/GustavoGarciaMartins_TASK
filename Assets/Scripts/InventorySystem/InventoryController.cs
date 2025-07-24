using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    public static InventoryController Instance;

    public GameObject[] Inventory = new GameObject[16];
    public GameObject ItemPrefab;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(Instance);
        }
    }

    public void AddItem(Item item) {
        GameObject itemGameObject = Instantiate(ItemPrefab, TryToFindEmptySlot().transform);
        itemGameObject.GetComponent<Item>().ItemData = item.ItemData;
        itemGameObject.GetComponent<Image>().sprite = item.ItemData.Icon;

    }

    private GameObject TryToFindEmptySlot() {
        for (int i = 0; i < Inventory.Length; i++) {
            if (Inventory[i].TryGetComponent<InventorySlotHandler>(out var slot)) {
                if (slot.IsEmpty) {
                    slot.SetEmptySlot(false);
                    return slot.gameObject;
                }
            }
        }

        return null;
    }

    public void ItemDrag() {
        Debug.Log("chegou aqui");
    }
}
