using TarodevController;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "Action_Knockback", menuName = "SO/State Machines/Actions/Knockback")]
public class KnockbackActionSO : StateActionSO<KnockbackAction>
{
    [SerializeField] float _force = 10f;

    public float Force { get => _force; }
}

public class KnockbackAction : StateAction
{
    private new KnockbackActionSO OriginSO => (KnockbackActionSO)base.OriginSO;

    private Rigidbody2D _rb = null;
    private PlayerController _controller = null;
    private PlayerWeaponHandler _weaponHandler = null;
    private CrouchHandler _crouchHandler = null;
    private HealthBehaviour _health = null;
    private float _timer = 0f;

    public override void Awake(StateMachine _stateMachine)
    {
        _rb = _stateMachine.GetComponent<Rigidbody2D>();
        _controller = _stateMachine.GetComponent<PlayerController>();
        _weaponHandler = _stateMachine.GetComponent<PlayerWeaponHandler>();
        _crouchHandler = _stateMachine.GetComponent<CrouchHandler>();
        _health = _stateMachine.GetComponent<HealthBehaviour>();
    }

    public override void OnStateEnter()
    {
        _rb.gravityScale = 1;
        _crouchHandler.EndCrouch();
        _weaponHandler.DisableShields();

        // flip to damage source
        // disable flip.

        //_health.HasTakenDamageThisFrame = false;
        _health.enabled = false;
        _timer = 0f;

        // get last damage source position.
        var _direction = (Vector2.left + Vector2.up).normalized;
        var _force = _direction * OriginSO.Force;
        _controller.ForceGrounded(false);
        _controller.StopVelocity();
        _controller.AddImpulse(_force);
    }

    public override void OnStateExit()
    {
        _rb.gravityScale = 0;
        _health.enabled = true;
    }

    public override void OnFixedUpdate()
    {
        //_controller.CheckCollisions();
        _timer += Time.fixedDeltaTime;

        if (_timer > 0.1f)
        {
            _controller.CheckCollisions();
        }

        //_controller.HandleJump();
        //_controller.HandleGravity();
    }

    public override void OnUpdate()
    {
        //_controller.IncreaseDeltaTime();
        //_controller.GatherInput();
    }
}
