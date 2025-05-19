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

    public override void Awake(StateMachine _stateMachine)
    {
        _controller = _stateMachine.GetComponent<PlayerController>();
        _weaponHandler = _stateMachine.GetComponent<PlayerWeaponHandler>();
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
        _weaponHandler.GatherInput();

        _controller.IncreaseDeltaTime();
        _controller.GatherInput();
    }
}
