using UnityEngine;
using UnityEngine.EventSystems;

public class UsableItem : MonoBehaviour, IPointerClickHandler
{
    public float DoubleClickTime = 0.3f;
    private float _lastClickTime;

    public delegate void OnUsedItemCallback(Item item);
    public event OnUsedItemCallback OnUsedItem;

    public void OnPointerClick(PointerEventData eventData) {
        if (eventData.button == PointerEventData.InputButton.Left) {
            float timeSinceLastClick = Time.time - _lastClickTime;

            if (timeSinceLastClick <= DoubleClickTime) {
                Debug.Log("Double Click Detected!");
                OnUsedItem?.Invoke(eventData.pointerPress.GetComponent<Item>());
            }            
            _lastClickTime = Time.time;
        }
    }
}
