using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlotHandler : MonoBehaviour, IDropHandler
{
    public bool IsEmpty { get; private set; }

    public void OnDrop(PointerEventData eventData) {
        if (!IsEmpty) return;

        var item = eventData.pointerDrag;

        item.transform.SetParent(this.transform);
        item.GetComponent<Image>().raycastTarget = true;
        item.transform.localPosition = new Vector3(0, 0, 0);

        var dragHandler = item.GetComponent<ItemDragHandler>();
        dragHandler.SetDropped();
        IsEmpty = false;
    }

    public void SetEmptySlot(bool isEmpty) {
        IsEmpty = isEmpty;
    }

}
