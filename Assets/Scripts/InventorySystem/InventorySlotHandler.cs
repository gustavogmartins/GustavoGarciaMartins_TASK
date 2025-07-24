using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlotHandler : MonoBehaviour, IDropHandler
{
    public bool IsEmpty;
    [SerializeField] private GameObject _currentSlotedItem;

    public void OnDrop(PointerEventData eventData) {
        var item = eventData.pointerDrag;

        if (IsEmpty && item != null) {
            item.transform.SetParent(this.transform);
            item.GetComponent<Image>().raycastTarget = true;
            item.transform.localPosition = new Vector3(0, 0, 0);

            var dragHandler = item.GetComponent<ItemDragHandler>();
            dragHandler.SetDropped();
            IsEmpty = false;
            _currentSlotedItem = item;

            Transform originalSlot = item.GetComponent<ItemDragHandler>().GetCurrentSlot();
            originalSlot.GetComponent<InventorySlotHandler>().IsEmpty = true;
        } else {
            Transform originalSlot = item.GetComponent<ItemDragHandler>().GetCurrentSlot();

            _currentSlotedItem.transform.SetParent(originalSlot);
            _currentSlotedItem.transform.localPosition = Vector3.zero;

            item.transform.SetParent(this.transform);
            item.GetComponent<Image>().raycastTarget = true;
            item.transform.localPosition = Vector3.zero;

            var dragHandler = item.GetComponent<ItemDragHandler>();
            dragHandler.SetDropped();
            IsEmpty = false;
            _currentSlotedItem = item;
        }

    }

    public void SetEmptySlot(bool isEmpty) {
        IsEmpty = isEmpty;
    }

    public void AddItem(GameObject item) {
        _currentSlotedItem = item;
        IsEmpty = false;
    }

}
