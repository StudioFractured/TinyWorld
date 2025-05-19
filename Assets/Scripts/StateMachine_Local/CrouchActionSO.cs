using TarodevController;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "Action_Crouch", menuName = "SO/State Machines/Actions/Crouch")]
public class CrouchActionSO : StateActionSO<CrouchAction>
{
    //
}

public class CrouchAction : StateAction
{
    private PlayerController _controller = null;
    private PlayerWeaponHandler _weaponHandler = null;
    private CrouchHandler _crouchHandler = null;

    public override void Awake(StateMachine _stateMachine)
    {
        _controller = _stateMachine.GetComponent<PlayerController>();
        _weaponHandler = _stateMachine.GetComponent<PlayerWeaponHandler>();
        _crouchHandler = _stateMachine.GetComponent<CrouchHandler>();
    }

    public override void OnStateEnter()
    {
        _controller.StopVelocity();
        _crouchHandler.StartCrouch();
    }

    public override void OnStateExit()
    {
        _crouchHandler.EndCrouch();
    }

    public override void OnFixedUpdate()
    {
        _controller.CheckCollisions();
        _controller.HandleJump();
        //_controller.HandleDirection();
        _controller.HandleGravity();
        //_controller.ApplyMovement();
    }

    public override void OnUpdate()
    {
        _weaponHandler.GatherInput();

        _controller.IncreaseDeltaTime();
        _controller.GatherInput();
    }
}
