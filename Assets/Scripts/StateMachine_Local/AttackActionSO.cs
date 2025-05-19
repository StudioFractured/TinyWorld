using TarodevController;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "Action_Attack", menuName = "SO/State Machines/Actions/Attack")]
public class AttackActionSO : StateActionSO<AttackAction>
{
    //
}

public class AttackAction : StateAction
{
    private PlayerController _controller = null;

    public override void Awake(StateMachine _stateMachine)
    {
        _controller = _stateMachine.GetComponent<PlayerController>();
    }

    public override void OnStateEnter()
    {
        _controller.StopVelocity();
    }

    public override void OnFixedUpdate()
    {
        //
    }

    public override void OnUpdate()
    {
        //
    }
}
