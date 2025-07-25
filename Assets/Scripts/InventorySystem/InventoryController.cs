using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    public static InventoryController Instance;

    public GameObject[] InventorySlots = new GameObject[15];
    public GameObject ItemPrefab;
    private string path;
    public ItemData[] AllItems;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(Instance);
        }

        path = Application.persistentDataPath + "/savedata.json";
    }

    private void Start() {
        LoadInventory();
    }

    public void AddItem(Item item) {
        GameObject slot = TryToFindEmptySlot();
        if (slot == null) { return; }

        GameObject itemGameObject = Instantiate(ItemPrefab, slot.transform);
        itemGameObject.GetComponent<Item>().ItemData = item.ItemData;
        itemGameObject.GetComponent<Image>().sprite = item.ItemData.Icon;

        slot.GetComponent<InventorySlotHandler>().AddItem(itemGameObject);

        if (itemGameObject.GetComponent<Item>().ItemData.IsUsable) {
            var usable = itemGameObject.AddComponent<UsableItem>();
            usable.OnUsedItem += GameObject.FindWithTag("Player").GetComponent<Player>().OnItemUsed;
        }

    }

    private GameObject TryToFindEmptySlot() {
        foreach (var slot in InventorySlots) {
            if (slot.GetComponent<InventorySlotHandler>().IsEmpty) {
                return slot;
            }
        }
        return null;
    }

    public void SaveInventory() {
        InventoryData save = new();

        for (int i = 0; i < InventorySlots.Length; i++) {
            var slot = InventorySlots[i].transform;
            if (slot.childCount > 1) {
                var item = slot.GetChild(1).GetComponent<Item>();
                save.slotItemIds[i] = item.ItemData.ID;
            } else {
                save.slotItemIds[i] = null;
            }
        }

        string json = JsonUtility.ToJson(save);
        File.WriteAllText(path, json);
        Debug.Log("Inventory saved.");
    }

    private void OnApplicationQuit() {
        SaveInventory();
    }

    public void LoadInventory() {
        if (!File.Exists(path))
            return;

        string json = File.ReadAllText(path);
        var save = JsonUtility.FromJson<InventoryData>(json);

        for (int i = 0; i < save.slotItemIds.Length; i++) {
            if (!string.IsNullOrEmpty(save.slotItemIds[i])) {
                ItemData data = System.Array.Find(AllItems, x => x.ID == save.slotItemIds[i]);
                if (data != null) {
                    GameObject item = Instantiate(ItemPrefab, InventorySlots[i].transform);
                    var itemComponent = item.GetComponent<Item>();
                    itemComponent.ItemData = data;
                    item.GetComponent<Image>().sprite = data.Icon;

                    UsableItem usable = item.AddComponent<UsableItem>();
                    usable.OnUsedItem += GameObject.FindWithTag("Player").GetComponent<Player>().OnItemUsed;

                    var slot = item.transform.parent.GetComponent<InventorySlotHandler>();
                    slot.GetComponent<InventorySlotHandler>().AddItem(item);
                }
            }
        }

        Debug.Log("Inventory Loaded.");
    }
}

public class InventoryData
{
    public string[] slotItemIds = new string[15];
}
