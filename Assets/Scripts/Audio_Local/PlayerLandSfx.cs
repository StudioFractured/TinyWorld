using TarodevController;
using UnityEngine;

public class PlayerLandSfx : AudioPlayer
{
    [SerializeField] PlayerController _controller = null;

    private void OnEnable()
    {
        _controller.GroundedChanged += Play;
    }

    private void OnDisable()
    {
        _controller.GroundedChanged -= Play;
    }

    private void Play(bool _grounded, float arg2)
    {
        if (_grounded)
        {
            Play();
        }
    }
}
