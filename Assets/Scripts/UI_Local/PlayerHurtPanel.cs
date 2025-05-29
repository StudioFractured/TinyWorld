using UnityEngine;

public class PlayerHurtPanel : CanvasView
{
    [SerializeField] PlayerHealth _health = null;

    private void Awake()
    {
        InstantHide();
    }

    private void OnEnable()
    {
        _health.OnDamageTaken += Play;
    }

    private void OnDisable()
    {
        _health.OnDamageTaken -= Play;
    }

    private void Play(float arg0)
    {
        InstantShow();
        Hide();
    }
}
