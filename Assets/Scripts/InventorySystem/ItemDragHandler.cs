using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDragHandler : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    private GameObject _inventorySlotsContainer;
    private Transform _currentSlot;
    private bool _wasDropped = false;

    public void OnDrag(PointerEventData eventData) {
        this.gameObject.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData) {
        this.gameObject.GetComponent<Image>().raycastTarget = true;

        if (!_wasDropped) {
            this.gameObject.transform.SetParent(_currentSlot);
            this.gameObject.transform.localPosition = Vector3.zero;
        } 

        this.gameObject.GetComponent<LayoutElement>().ignoreLayout = false;
    }

    public void OnBeginDrag(PointerEventData eventData) {
        _wasDropped = false;
        _inventorySlotsContainer = GameObject.FindGameObjectWithTag("InventorySlotsContainer");
        _currentSlot = this.gameObject.transform.parent;

        this.gameObject.GetComponent<Image>().raycastTarget = false;
        this.gameObject.GetComponent<LayoutElement>().ignoreLayout = true;
        this.gameObject.transform.SetParent(_inventorySlotsContainer.transform);
        this.gameObject.transform.SetAsLastSibling();
    }

    public void SetDropped() {
        _wasDropped = true;
    }

    public Transform GetCurrentSlot() {
        return _currentSlot;
    }
}
