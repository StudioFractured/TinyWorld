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
    //private PlayerController _controller = null;
    private PlayerInputHandler _input = null;

    public override void Awake(StateMachine _stateMachine)
    {
        //_controller = _stateMachine.GetComponent<PlayerController>();
        _input = _stateMachine.GetComponent<PlayerInputHandler>();
    }

    protected override bool Statement()
    {
        return _input.Move.y < -0.5f || _input.CrouchPressed;
        //return _controller.FrameInput.y < -0.5f;
    }
}
