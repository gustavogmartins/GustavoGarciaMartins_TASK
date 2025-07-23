using UnityEngine;
using UnityEngine.InputSystem;

public abstract class NPC : MonoBehaviour, IInteractable
{
    [SerializeField] private SpriteRenderer _interactSprite;

    private Transform _playerTransform;

    private void Start() {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update() {
        if (Keyboard.current.eKey.wasPressedThisFrame && IsInRangeToInteract()) {
            Interact();
        }

        if (_interactSprite.gameObject.activeSelf && !IsInRangeToInteract()) {
            _interactSprite.gameObject.SetActive(false);
        } else if (!_interactSprite.gameObject.activeSelf && IsInRangeToInteract()) {
            _interactSprite.gameObject.SetActive(true);
        }
    }

    public abstract void Interact();

    private bool IsInRangeToInteract() {
        if (Vector2.Distance(_playerTransform.position, transform.position) < 2f) {
            return true;
        } else {
            return false;
        }
    }
}
