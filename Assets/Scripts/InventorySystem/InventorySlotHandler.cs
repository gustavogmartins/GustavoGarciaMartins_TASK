using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlotHandler : MonoBehaviour, IDropHandler
{
    public bool IsEmpty => _currentSlotedItem == null;
    [SerializeField] private GameObject _currentSlotedItem;

    public void OnDrop(PointerEventData eventData) {
        var itemDragged = eventData.pointerDrag;

        if (itemDragged == null) { return; }

        var dragHandler = itemDragged.GetComponent<ItemDragHandler>();
        Transform originalSlot = dragHandler.GetCurrentSlot();
        var originalSlotHandler = originalSlot.GetComponent<InventorySlotHandler>();

        if (_currentSlotedItem != null) {
            GameObject itemToSwap = _currentSlotedItem;

            itemToSwap.transform.SetParent(originalSlot);
            itemToSwap.transform.localPosition = Vector3.zero;
            itemToSwap.GetComponent<ItemDragHandler>().SetCurrentSlot(originalSlot);
            originalSlotHandler._currentSlotedItem = itemToSwap;
        } else {
            originalSlotHandler._currentSlotedItem = null;
        }

        itemDragged.transform.SetParent(this.transform);
        itemDragged.transform.localPosition = Vector3.zero;
        itemDragged.GetComponent<Image>().raycastTarget = true;
        dragHandler.SetDropped();
        dragHandler.SetCurrentSlot(this.transform);
        _currentSlotedItem = itemDragged;
    }

    public void AddItem(GameObject item) {
        _currentSlotedItem = item;
    }

}
