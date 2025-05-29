using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] AudioSO _so = null;
    [SerializeField] bool _playOnStart = false;
    [SerializeField] bool _stopOnDisable = false;

    private AudioSource _audioSource = null;

    private void Start()
    {
        if (_playOnStart)
        {
            Play();
        }
    }

    private void OnDisable()
    {
        if (_audioSource == null) return;

        if (_stopOnDisable)
        {
            _audioSource.Stop();
        }
    }

    [ContextMenu("Play()")]
    public void Play()
    {
        _audioSource = _so.Play(transform.position);
    }
}
