using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class HealthBehaviour : MonoBehaviour
{
    [SerializeField] float _maxValue = 10f;
    [SerializeField] float _initialValue = 5f;

    [Header("// READONLY")]
    [SerializeField] public float _currentValue = 0f;
    [SerializeField] GameObject _lastDamageSource = null;
    [SerializeField] bool _hasTakenDamageThisFrame = false;
    public bool isPlayer = false;
    public bool IsChest = false;

    public event UnityAction<float> OnHealed = null;
    public event UnityAction<float> OnDamageTaken = null;
    public event UnityAction OnDie = null;

    public GameObject LastDamageSource => _lastDamageSource;
    public bool HasTakenDamageThisFrame => _hasTakenDamageThisFrame;

    [Header("Damage Flash Settings")]
    public Color FlashColor = Color.white;
    public float FlashTime = 0.1f;

    public SpriteRenderer _spriteRenderer;
    public Material _material;

    

    private void Awake()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _material = new Material(_spriteRenderer.material);
        _spriteRenderer.material = _material; // ✅ Assign the material back

        if (gameObject.GetComponent<ChestContents>() != null) { IsChest = true; }


        ResetValue();
    }

    private void LateUpdate()
    {
        _hasTakenDamageThisFrame = false;
    }

    public void RestoreHealth(float value)
    {
        _currentValue += value;
        _currentValue = Mathf.Clamp(_currentValue, 0f, _maxValue);
        OnHealed?.Invoke(_currentValue);
    }

    public void TakeDamage(GameObject source, float value)
    {
        if (IsChest)
        {
            /*ChestContents _chest = GetComponent<ChestContents>();

            if (gameObject.tag == "common")
                _chest.OpenChest(_chest.gameObject, new Vector2(1f, 3f), new Vector2(2f, 4f));
            else if (_chest.gameObject.tag == "rare")
                _chest.OpenChest(gameObject, new Vector2(2f, 4f), new Vector2(3f, 6f));
            else if (_chest.gameObject.tag == "epic")
                _chest.OpenChest(gameObject, new Vector2(3f, 5f), new Vector2(4f, 8f));
            Debug.Log("WOOOOOOO");*/
        }

        if (!enabled) return;

        _lastDamageSource = source;
        _currentValue -= value;
        _currentValue = Mathf.Clamp(_currentValue, 0f, _maxValue);

        StartCoroutine(DamageFlash()); // ✅ Correctly start the coroutine

        if (_currentValue <= 0)
        {
            Die();
        }
        else
        {
            if (isPlayer)
            {
                PlayerHealth ph = FindAnyObjectByType<PlayerHealth>();
                ph.ShakeCam();
            }

            _hasTakenDamageThisFrame = true;
            OnDamageTaken?.Invoke(_currentValue);
        }
    }


    public void ResetValue()
    {
        _currentValue = _initialValue;
    }

    public virtual void Die()
    {
        StartCoroutine(DelayedDeath());
    }

    private IEnumerator DelayedDeath()
    {
        //yield return null;
        yield return new WaitForSeconds(0.1f);
        OnDie?.Invoke();
        gameObject.SetActive(false);
    }

    public void IncreaseMaxValue()
    {
        _maxValue++;
        RestoreHealth(1);
    }

    [ContextMenu("// TakeDamage()")]
    public void TakeDamage()
    {
        TakeDamage(gameObject, 0);
    }

    private IEnumerator DamageFlash()
    {
        SetFlashColor();

        SetFlashAmount(5f);
        yield return new WaitForSeconds(FlashTime);
        SetFlashAmount(0.5f);
    }

    private void SetFlashColor()
    {
        _material.SetColor("_FlashColor", FlashColor);
    }

    private void SetFlashAmount(float amount)
    {
        _material.SetFloat("_FlashAmount", amount);
    }
}
