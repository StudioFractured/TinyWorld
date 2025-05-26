using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    [SerializeField] bool _useSign = true;
    [SerializeField] bool _rotateToDirection = false;

    private Vector3 direction;

    public void SetTarget(Vector3 target)
    {
        if (_useSign)
        {
            float dirX = Mathf.Sign(target.x - transform.position.x);
            direction = new Vector3(dirX, 0f, 0f);
        }
        else
        {
            var _targetDirection = (target - transform.position).normalized;
            direction = _targetDirection;
        }

        Destroy(gameObject, 10f);
    }

    private void Update()
    {
        transform.position += speed * Time.deltaTime * direction;

        if (_rotateToDirection)
            transform.right = (Vector2)direction;
    }
}
