using UnityEngine;

public class CrouchHandler : MonoBehaviour
{
    //[SerializeField] Transform _target = null;
    [SerializeField] Collider2D _defaultCollider = null;
    [SerializeField] Collider2D _crouchCollider = null;
    [SerializeField] PlayerAnim _anim = null;

    private void Awake()
    {
        EndCrouch();
    }

    public void StartCrouch()
    {
        _defaultCollider.enabled = false;
        _crouchCollider.enabled = true;
        _anim.SetIsCrouching(true);
    }

    public void EndCrouch()
    {
        _defaultCollider.enabled = true;
        _crouchCollider.enabled = false;
        _anim.SetIsCrouching(false);
    }
}
