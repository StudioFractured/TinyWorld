using UnityEngine;

public class FlipOnMovement : SideFlipper
{
    [SerializeField] Rigidbody2D _rb = null;

    private void LateUpdate()
    {
        if (_rb.linearVelocity.x > 0)
        {
            Flip(true);
        }
        else if (_rb.linearVelocityX < 0)
        {
            Flip(false);
        }
    }
}
