using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] AudioSO _so = null;
    [SerializeField] bool _playOnStart = false;

    private void Start()
    {
        if (_playOnStart)
        {
            Play();
        }
    }

    [ContextMenu("Play()")]
    public void Play()
    {
        _so.Play(transform.position);
    }
}
