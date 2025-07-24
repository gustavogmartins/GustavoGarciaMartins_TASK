using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    public static InventoryController Instance;

    public GameObject[] InventorySlot = new GameObject[16];
    public GameObject ItemPrefab;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(Instance);
        }
    }

    private void Start() {
        foreach (GameObject inventorySlot in InventorySlot) {
            inventorySlot.GetComponent<InventorySlotHandler>().SetEmptySlot(true);
        }
    }

    public void AddItem(Item item) {
        GameObject emptySlot = TryToFindEmptySlot();
        if (emptySlot == null) { return; }

        GameObject itemGameObject = Instantiate(ItemPrefab, emptySlot.transform);
        itemGameObject.GetComponent<Item>().ItemData = item.ItemData;
        itemGameObject.GetComponent<Image>().sprite = item.ItemData.Icon;
        emptySlot.GetComponent<InventorySlotHandler>().SetEmptySlot(false);
    }

    private GameObject TryToFindEmptySlot() {
        foreach (var slot in InventorySlot) {
            if (slot.GetComponent<InventorySlotHandler>().IsEmpty) {
                return slot;
            }
        }
        return null;
    }
}
