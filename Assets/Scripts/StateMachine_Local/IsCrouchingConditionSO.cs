using TarodevController;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "Condition_IsCrouching", menuName = "SO/State Machines/Conditions/Is Crouching")]
public class IsCrouchingConditionSO : StateConditionSO<IsCrouchingCondition>
{
    //
}

public class IsCrouchingCondition : Condition
{
    private PlayerController _controller = null;

    public override void Awake(StateMachine _stateMachine)
    {
        _controller = _stateMachine.GetComponent<PlayerController>();
    }

    protected override bool Statement()
    {
        return _controller.FrameInput == Vector2.down;
    }
}
