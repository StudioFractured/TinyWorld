using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] InputActionReference _moveAction = null;
    [SerializeField] InputActionReference _jumpAction = null;
    [SerializeField] InputActionReference _attackAction = null;
    [SerializeField] InputActionReference _defendAction = null;

    [Header("// READONLY")]
    [SerializeField] Vector2 _move = default;

    public Vector2 Move { get => _move; }
    public bool JumpPerformed { get => _jumpAction.action.WasPerformedThisFrame(); }
    public bool JumpPressed { get => _jumpAction.action.IsPressed(); }
    public bool AttackPerformed { get => _attackAction.action.WasPerformedThisFrame(); }
    public bool DefendPressed { get => _defendAction.action.IsPressed(); }

    private void OnEnable()
    {
        PauseView.OnPauseChanged += PauseMenu_OnPauseChanged;
    }

    private void PauseMenu_OnPauseChanged(bool _isPaused)
    {
        var _map = _moveAction.asset.FindActionMap("Player");

        if (_isPaused)
            _map.Disable();
        else
            _map.Enable();
    }

    private void Update()
    {
        _move = _moveAction.action.ReadValue<Vector2>();
    }
}
