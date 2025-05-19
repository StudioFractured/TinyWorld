using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class HealthBehaviour : MonoBehaviour
{
    [Header("HP and Damage")]
    public int HP = 3;
    public int Damage = 1;
    public float effectDuration = 1f;
    public float shrinkFactor = 0.9f;
    public float ShrinkDuration = 0.2f;

    [Header("Smoke")]
    public GameObject SmokePrefab;
    public GameObject SpawnPointSmoke;
    private Vector3 SpawnPoint;

    private bool CanTakeDMG = true;

    [Header("// READONLY")]
    [SerializeField] float _currentValue = 0f;

    public event UnityAction<float> OnDamageTaken = null;

    private void Awake()
    {
        ResetValue();
    }

    public void TakeDamage(int dmg)
    {
        StartCoroutine(DMGCoroutine(dmg));
    }

    public IEnumerator DMGCoroutine(int dmg)
    {
        if (CanTakeDMG)
        {
            CanTakeDMG = false;
            HP -= dmg;

            yield return StartCoroutine(DamageShrink());

            if (HP <= 0)
            {
                HandleDeath();
            }

            CanTakeDMG = true;
        }
    }


    private void HandleDeath()
    {
        Destroy(gameObject.GetComponent<Collider>());

        SpawnPoint = SpawnPointSmoke.transform.position;

        if (SmokePrefab != null && SpawnPointSmoke != null)
        {
            Instantiate(SmokePrefab, SpawnPoint, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("SmokePrefab or SpawnPointSmoke not assigned.");
        }

        Destroy(gameObject);
    }

    private IEnumerator DamageShrink()
    {
        Vector3 originalScale = transform.localScale;
        Vector3 targetScale = originalScale * shrinkFactor;

        float halfDuration = ShrinkDuration / 2f;
        float timer = 0f;

        // Shrink phase
        while (timer < halfDuration)
        {
            float t = timer / halfDuration;
            transform.localScale = Vector3.Lerp(originalScale, targetScale, t);
            timer += Time.deltaTime;
            yield return null;
        }
        transform.localScale = targetScale;

        // Return phase
        timer = 0f;
        while (timer < halfDuration)
        {
            float t = timer / halfDuration;
            transform.localScale = Vector3.Lerp(targetScale, originalScale, t);
            timer += Time.deltaTime;
            yield return null;
        }
        transform.localScale = originalScale;
    }

    public void ResetValue()
    {
        _currentValue = HP;
    }

    public virtual void Die()
    {
        gameObject.SetActive(false);
    }
}
