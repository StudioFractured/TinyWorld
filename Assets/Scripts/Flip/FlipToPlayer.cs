using TarodevController;
using UnityEngine;

public class FlipToPlayer : SideFlipper
{
    [Header("// Readonly")]
    [SerializeField] PlayerController _player = null;

    private void Awake()
    {
        _player = FindFirstObjectByType<PlayerController>();
    }

    private void LateUpdate()
    {
        bool _isPlayerToTheRight = _player.transform.position.x >= transform.position.x;
        Flip(_isPlayerToTheRight);
    }
}
