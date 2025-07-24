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

    public void AddItem(Item item) {
        GameObject slot = TryToFindEmptySlot();
        if (slot == null) { return; }

        GameObject itemGameObject = Instantiate(ItemPrefab, slot.transform);
        itemGameObject.GetComponent<Item>().ItemData = item.ItemData;
        itemGameObject.GetComponent<Image>().sprite = item.ItemData.Icon;

        slot.GetComponent<InventorySlotHandler>().AddItem(itemGameObject);

        if (itemGameObject.GetComponent<Item>().ItemData.IsUsable) {
            itemGameObject.AddComponent<UsableItem>();
            UsableItem.Instance.OnUsedItem += GameObject.FindWithTag("Player").GetComponent<Player>().OnItemUsed;
        }

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
