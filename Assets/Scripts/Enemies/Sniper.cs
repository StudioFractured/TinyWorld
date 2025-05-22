using UnityEngine;
using System.Collections;

public class Ranger : MonoBehaviour, IReactOnHit
{
    [Header("References")]
    public Transform basePoint;
    public Transform player;
    public GameObject bulletPrefab;
    public Transform firePoint;

    [Header("Movement")]
    public float idleSpeed = 2f;
    public float hoverSpeed = 2f;
    public float hoverHeight = 0.5f;
    public float idleRange = 3f;
    public float idleTargetRefreshTime = 3f;

    [Header("Combat")]
    public float attackRange = 10f;
    public float fireCooldown = 1.5f;

    private Vector3 idleTarget;
    private float idleTimer;
    private float fireTimer;
    private float hoverOffset;
    private float baseY;
    public bool isFrozen = false;
    public void ReactToHit()
    {
        StartCoroutine(FreezeMovement(0.1f));
    }

    private IEnumerator FreezeMovement(float duration)
    {
        isFrozen = true;
        yield return new WaitForSeconds(duration);
        isFrozen = false;
    }

    private void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
            if (player == null)
            {
                Debug.LogError("Player not assigned and not found via tag.");
            }
        }

        baseY = transform.position.y;
        hoverOffset = Random.Range(0f, 2f * Mathf.PI);
        PickNewIdleTarget();
    }

    private void Update()
    {
        if (isFrozen) return;
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(player.position, transform.position);
        fireTimer -= Time.deltaTime;

        // Calculate vertical hover
        float hoverY = Mathf.Sin(Time.time * hoverSpeed + hoverOffset) * hoverHeight;
        float newY = basePoint.position.y + hoverY;

        if (distanceToPlayer <= attackRange)
        {
            if (fireTimer <= 0f)
            {
                Fire();
                fireTimer = fireCooldown;
            }

            // Apply hover while staying in place
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }
        else
        {
            IdleMovement(newY);
        }
    }

    private void IdleMovement(float hoverY)
    {
        idleTimer -= Time.deltaTime;

        if (idleTimer <= 0f || Vector3.Distance(transform.position, idleTarget) < 0.2f)
        {
            PickNewIdleTarget();
        }

        Vector3 hoverTarget = new Vector3(idleTarget.x, hoverY, idleTarget.z);
        transform.position = Vector3.MoveTowards(transform.position, hoverTarget, idleSpeed * Time.deltaTime);
    }

    private void PickNewIdleTarget()
    {
        Vector2 randomCircle = Random.insideUnitCircle * idleRange;
        idleTarget = basePoint.position + new Vector3(randomCircle.x, 0, randomCircle.y);
        idleTimer = idleTargetRefreshTime;
    }

    private void Fire()
    {
        if (bulletPrefab == null || firePoint == null) return;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.SetTarget(player.position);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (basePoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(basePoint.position, idleRange);
        }
    }
}
