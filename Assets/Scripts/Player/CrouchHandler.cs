using UnityEngine;

public class CrouchHandler : MonoBehaviour
{
    [SerializeField] Transform _target = null;
    [SerializeField] Collider2D _defaultCollider = null;
    [SerializeField] Collider2D _crouchCollider = null;

    private void Awake()
    {
        EndCrouch();
    }

    public void StartCrouch()
    {
        var _newScale = _target.localScale;
        _newScale.y = 0.5f;
        _target.localScale = _newScale;

        _defaultCollider.enabled = false;
        _crouchCollider.enabled = true;
    }

    public void EndCrouch()
    {
        var _newScale = _target.localScale;
        _newScale.y = 1f;
        _target.localScale = _newScale;

        _defaultCollider.enabled = true;
        _crouchCollider.enabled = false;
    }
}
