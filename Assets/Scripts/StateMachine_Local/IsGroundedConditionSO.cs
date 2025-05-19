using TarodevController;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "Condition_IsGrounded", menuName = "SO/State Machines/Conditions/Is Grounded")]
public class IsGroundedConditionSO : StateConditionSO<IsGroundedCondition>
{
    //
}

public class IsGroundedCondition : Condition
{
    private PlayerController _controller = null;

    public override void Awake(StateMachine _stateMachine)
    {
        _controller = _stateMachine.GetComponent<PlayerController>();
    }

    protected override bool Statement()
    {
        return _controller.Grounded;
    }
}
