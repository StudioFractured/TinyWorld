using TarodevController;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "Action_Knockback", menuName = "SO/State Machines/Actions/Knockback")]
public class KnockbackActionSO : StateActionSO<KnockbackAction>
{
    [SerializeField] ScriptableStats _stats = null;

    public ScriptableStats Stats { get => _stats; }
}

public class KnockbackAction : StateAction
{
    private new KnockbackActionSO OriginSO => (KnockbackActionSO)base.OriginSO;

    private PlayerController _controller = null;
    private PlayerWeaponHandler _weaponHandler = null;
    private CrouchHandler _crouchHandler = null;
    private HealthBehaviour _health = null;
    private SideFlipper _flipper = null;
    private float _timer = 0f;

    public override void Awake(StateMachine _stateMachine)
    {
        _controller = _stateMachine.GetComponent<PlayerController>();
        _weaponHandler = _stateMachine.GetComponent<PlayerWeaponHandler>();
        _crouchHandler = _stateMachine.GetComponent<CrouchHandler>();
        _health = _stateMachine.GetComponent<HealthBehaviour>();
        _flipper = _stateMachine.GetComponent<SideFlipper>();
    }

    public override void OnStateEnter()
    {
        _crouchHandler.EndCrouch();
        _weaponHandler.DisableShields();

        var _xDirection = _health.LastDamageSource.transform.position.x >= _controller.transform.position.x ? -1f : 1f;
        _controller.StartKnockBack(_xDirection, OriginSO.Stats);

        _flipper.Flip(_xDirection < 0);
        _flipper.enabled = false;
        _health.enabled = false;
        _timer = 0f;
    }

    public override void OnStateExit()
    {
        _controller.StopVelocity();
        _controller.ResetStats();
        _flipper.enabled = true;
        _health.enabled = true;
    }

    public override void OnFixedUpdate()
    {
        _timer += Time.fixedDeltaTime;

        if (_timer > 0.1f)
        {
            _controller.CheckCollisions();
        }

        _controller.HandleGravity();
        _controller.ApplyMovement();
    }

    public override void OnUpdate()
    {
        _controller.IncreaseDeltaTime();
        _controller.GatherInput();
    }
}
