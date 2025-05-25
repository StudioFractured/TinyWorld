using TarodevController;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "Action_Attack", menuName = "SO/State Machines/Actions/Attack")]
public class AttackActionSO : StateActionSO<AttackAction>
{
    [SerializeField] bool _isCrouchAttack = false;

    public bool IsCrouchAttack { get => _isCrouchAttack; }
}

public class AttackAction : StateAction
{
    private new AttackActionSO OriginSO => (AttackActionSO)base.OriginSO;

    private PlayerController _controller = null;
    private CrouchHandler _crouchHandler = null;
    private PlayerAnim _anim = null;

    public override void Awake(StateMachine _stateMachine)
    {
        _controller = _stateMachine.GetComponent<PlayerController>();
        _crouchHandler = _stateMachine.GetComponent<CrouchHandler>();
        _anim = _stateMachine.GetComponent<PlayerAnim>();
    }

    public override void OnStateEnter()
    {
        if (OriginSO.IsCrouchAttack)
        {
            _crouchHandler.StartCrouch();
        }

        _controller.StopVelocity();
        _anim.SetAttackTrigger();

    }

    public override void OnFixedUpdate()
    {
        //
    }

    public override void OnUpdate()
    {
        _controller.IncreaseDeltaTime();
        _controller.GatherInput();
    }
}
