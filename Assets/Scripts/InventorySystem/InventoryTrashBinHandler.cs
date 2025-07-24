using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryTrashBinHandler : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData) {
        if (eventData.pointerDrag.GetComponent<Item>()) {
            Destroy(eventData.pointerDrag);
        }
    }
}
