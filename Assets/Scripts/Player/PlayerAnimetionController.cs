using UnityEngine;

public class PlayerAnimetionController : MonoBehaviour
{
    [SerializeField] Animator _animator;

    private void Start() {
        Player.Instance.OnPlayerMove += OnPlayerMove;
        Player.Instance.OnPlayerRoll += OnPlayerRoll;
    }

    private void OnPlayerMove() {
        if (Player.Instance.IsMoving == true) {
            _animator.SetInteger("Transition", 1);
        } else {
            _animator.SetInteger("Transition", 0);
        }
    }

    private void OnPlayerRoll() {
        if (Player.Instance.IsMoving == false) { return; }
        _animator.SetTrigger("IsPlayerRolling");
    }
}
