using TarodevController;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class PlayerAnim : MonoBehaviour
{
    [SerializeField] PlayerController _controller = null;
    [SerializeField] Animator _anim = null;

    private void LateUpdate()
    {
        _anim.SetBool($"IsGrounded", _controller.Grounded);

        if (_controller.Grounded)
        {
            var _xValue = Mathf.Abs(_controller.GetLinearVelocity().x);
            _anim.SetFloat($"XVelocity", _xValue);
        }
        else
        {
            var _yValue = _controller.GetLinearVelocity().y;
            _anim.SetFloat("YVelocity", _yValue);
        }
    }

    public void SetIsCrouching(bool _value)
    {
        _anim.SetBool("IsCrouching", _value);
    }

    public void SetAttackTrigger()
    {
        _anim.SetTrigger("Attack");
    }

    public void SetIsAttacking(bool _value)
    {
        _anim.SetBool("IsAttack", _value);
    }
}
