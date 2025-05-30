using TarodevController;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class PlayerAnim : MonoBehaviour
{
    [SerializeField] PlayerController _controller = null;
    [SerializeField] Animator _anim = null;

    private readonly int IS_GROUNDED_HASH = Animator.StringToHash("IsGrounded");
    private readonly int X_VELOCITY_HASH = Animator.StringToHash("XVelocity");
    private readonly int Y_VELOCITY_HASH = Animator.StringToHash("YVelocity");
    private readonly int IS_CROUCHING_HASH = Animator.StringToHash("IsCrouching");
    private readonly int ATTACK_HASH = Animator.StringToHash("Attack");

    private void LateUpdate()
    {
        _anim.SetBool(IS_GROUNDED_HASH, _controller.Grounded);

        if (_controller.Grounded)
        {
            var _xValue = Mathf.Abs(_controller.GetLinearVelocity().x);
            _anim.SetFloat(X_VELOCITY_HASH, _xValue);
        }
        else
        {
            var _yValue = _controller.GetLinearVelocity().y;
            _anim.SetFloat(Y_VELOCITY_HASH, _yValue);
        }
    }

    public void SetIsCrouching(bool _value)
    {
        _anim.SetBool(IS_CROUCHING_HASH, _value);
    }

    public void SetAttackTrigger()
    {
        _anim.SetTrigger(ATTACK_HASH);
    }
}
