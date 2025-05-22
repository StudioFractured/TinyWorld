using System.Collections;
using UnityEngine;

public class MoveInsideArea : MonoBehaviour
{
    [SerializeField] Collider2D _collider = null;
    [SerializeField] float _speed = 5f;
    [SerializeField] float _waitTime = 2f;

    [Header("// READONLY")]
    [SerializeField] Vector2 _targetPos = default;
    [SerializeField] bool _isWaiting = false;

    private void Awake()
    {
        UpdateTargetPosition();
    }

    private void Update()
    {
        if (_isWaiting) return;

        transform.position = Vector2.MoveTowards(transform.position, _targetPos, _speed * Time.deltaTime);

        if ((Vector2)transform.position == _targetPos)
        {
            StartCoroutine(Wait_Routine(_waitTime));
            UpdateTargetPosition();
        }
    }

    private void UpdateTargetPosition()
    {
        _targetPos = GeneralMethods.RandomPointInBounds(_collider.bounds);
    }

    private IEnumerator Wait_Routine(float _duration)
    {
        _isWaiting = true;
        yield return new WaitForSeconds(_duration);
        _isWaiting = false;
    }

    public void StopMoving(float _duration)
    {
        StartCoroutine(Wait_Routine(_duration));
    }

    public bool IsMoving()
    {
        return !_isWaiting;
    }
}
