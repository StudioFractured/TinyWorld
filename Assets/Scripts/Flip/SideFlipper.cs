using UnityEngine;

public class SideFlipper : MonoBehaviour
{
    [SerializeField] Transform _target = null;

    public void Flip(bool _toTheRight)
    {
        var _x = _toTheRight ? 1 : -1;
        _target.localScale = new Vector3(_x, 1, 1);
    }

    public bool IsFacingRight()
    {
        return _target.localScale.x >= 0;
    }
}
