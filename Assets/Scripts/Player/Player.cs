using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public static Player Instance;
    public float MovementSpeed;

    [SerializeField] private Rigidbody2D _rigidBody;
    private Vector2 _direction;

    public bool IsMoving { get; private set; }
    public bool IsRolling { get; private set; }

    public delegate void OnPlayerMoveCallback();
    public event OnPlayerMoveCallback OnPlayerMove;

    public delegate void OnPlayerRollCallback();
    public event OnPlayerRollCallback OnPlayerRoll;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }

    }

    private void Update() {
        OnInput();
        OnRoll();
    }

    private void FixedUpdate() {
        PlayerMove();
    }

    private void OnRoll() {
        if (Keyboard.current.spaceKey.wasPressedThisFrame) {
            IsRolling = true;
            OnPlayerRoll?.Invoke();
        }

        if (Keyboard.current.spaceKey.wasReleasedThisFrame) {
            IsRolling = false;
        }
    }

    private void OnInput() {
        _direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    private void PlayerMove() {
        _rigidBody.MovePosition(_rigidBody.position + _direction.normalized * MovementSpeed * Time.fixedDeltaTime);

        if (_direction.sqrMagnitude > 0) {
            IsMoving = true;

            if (_direction.x > 0) {
                transform.eulerAngles = new Vector2(0, 0);
            }

            if (_direction.x < 0) {
                transform.eulerAngles = new Vector2(0, 180);
            }
        } else {
            IsMoving = false;
        }

        OnPlayerMove?.Invoke();
    }
}
