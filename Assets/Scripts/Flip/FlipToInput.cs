using UnityEngine;

public class FlipToInput : SideFlipper
{
    [SerializeField] PlayerInputHandler _input = null;

    private void LateUpdate()
    {
        var _horizontalInput = _input.Move.x;

        if (_horizontalInput > 0)
        {
            Flip(true);
        }
        else if (_horizontalInput < 0)
        {
            Flip(false);
        }
    }
}
