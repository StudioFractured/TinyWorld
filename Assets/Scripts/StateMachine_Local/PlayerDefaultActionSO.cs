using TarodevController;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "Action_PlayerDefault", menuName = "SO/State Machines/Actions/Player Default")]
public class PlayerDefaultActionSO : StateActionSO<PlayerDefaultAction>
{
    //
}

public class PlayerDefaultAction : StateAction
{
    private PlayerController _controller = null;
    private PlayerWeaponHandler _weaponHandler = null;
    private CrouchHandler _crouchHandler = null;
    private PlayerAnim _anim = null;

    private bool _isAirAttacking = false;

    public override void Awake(StateMachine _stateMachine)
    {
        _controller = _stateMachine.GetComponent<PlayerController>();
        _weaponHandler = _stateMachine.GetComponent<PlayerWeaponHandler>();
        _crouchHandler = _stateMachine.GetComponent<CrouchHandler>();
        _anim = _stateMachine.GetComponent<PlayerAnim>();
    }

    public override void OnStateEnter()
    {
        _crouchHandler.EndCrouch();
        _weaponHandler.DisableAll();
    }

    public override void OnFixedUpdate()
    {
        _controller.CheckCollisions();
        _controller.HandleJump();
        _controller.HandleDirection();
        _controller.HandleGravity();
        _controller.ApplyMovement();
    }

    public override void OnUpdate()
    {
        _weaponHandler.GatherStandInput();
        HandleAirAttack();

        _controller.IncreaseDeltaTime();
        _controller.GatherInput();
    }

    private void HandleAirAttack()
    {
        if (_weaponHandler.IsAttacking() && !_controller.Grounded && !_isAirAttacking)
        {
            _isAirAttacking = true;
            _anim.SetAttackTrigger();
        }

        if (_controller.Grounded)
        {
            _isAirAttacking = false;
        }
    }
}
