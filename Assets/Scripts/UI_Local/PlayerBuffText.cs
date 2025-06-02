using DG.Tweening;
using TMPro;
using UnityEngine;

public class PlayerBuffText : MonoBehaviour
{
    [SerializeField] Transform _root = null;
    [SerializeField] TextMeshProUGUI _text = null;
    [SerializeField] float _yOffset = 1;
    [SerializeField] float _moveDuration = 1;
    [SerializeField] float _fadeDuration = 1;

    private Vector2 _defaultPosition = default;

    private void Awake()
    {
        _defaultPosition = _text.transform.localPosition;
        _text.enabled = false;
    }

    private void LateUpdate()
    {
        _text.transform.localScale = _root.localScale;
    }

    //[ContextMenu("// Play()")]
    //public void Play()
    //{
    //    Play("Max Health Up", Color.red);
    //}

    public void Play(string _message/*, Color _color*/)
    {
        _text.enabled = true;
        _text.text = _message;
        //_text.color = _color;

        _text.DOComplete();
        _text.transform.DOComplete();
        _text.color = Color.white;
        _text.transform.localPosition = _defaultPosition;

        _text.transform.DOLocalMoveY(_defaultPosition.y + _yOffset, _moveDuration).
            OnComplete(() =>
            {
                _text.DOFade(0, _fadeDuration);
                //_text.enabled = false;
            });
    }
}
