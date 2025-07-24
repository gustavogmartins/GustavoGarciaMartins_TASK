using UnityEngine;
using UnityEngine.InputSystem;

public class UiManager : MonoBehaviour
{
    [SerializeField] private GameObject InventoryPanel;

    public Transform PlayerTarget;
    public Vector3 offset = new Vector3(0, 0, 0);
    public Transform HealthBarCanvas;

    void Update() {        
        if (Keyboard.current.iKey.wasPressedThisFrame) {
            if (!InventoryPanel.activeSelf) {
                InventoryPanel.SetActive(true);
            } else {
                InventoryPanel.SetActive(false);
            }
        }
    }

    void LateUpdate() {
        HealthBarCanvas.transform.position = PlayerTarget.position + offset;
    }
}
