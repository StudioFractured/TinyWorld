using UnityEngine;
using System.Collections;

public class BossEnemy : MonoBehaviour, IReactOnHit
{
    [Header("References")]
    public Transform[] patrolPoints;
    public Transform player;
    public Bullet bulletPrefab;
    public Transform firePoint;

    [Header("Movement")]
    public float moveSpeed = 3f;
    public float tiltAmount = 15f;
    public float swayHeight = 0.3f;
    public float swaySpeed = 2f;
    private int currentPointIndex = 0;
    private Vector3 lastPosition;

    [Header("Attack")]
    public float fireCooldown = 3f;
    public float burstInterval = 0.15f;
    public int burstCount = 1;
    public int bulletsPerBurst = 3;
    public float waitAtPointDuration = 1.5f;
    public float offsetMin = -0.6f;
    public float offsetMax = 0.6f;
    private bool hasFiredAtPoint = false;
    private bool isFiring = false;

    [Header("// FREEZE")]
    [SerializeField] float _frozeDuration = 0.3f;
    [SerializeField] bool isFrozen = false;

    private float swayOffset;

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

        lastPosition = transform.position;
        swayOffset = Random.Range(0f, Mathf.PI * 2);
    }

    private void Update()
    {
        if (isFrozen || isFiring) return;
        if (player == null || patrolPoints.Length == 0) return;

        Transform targetPoint = patrolPoints[currentPointIndex];
        Vector3 direction = (targetPoint.position - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, targetPoint.position);

        if (distance > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, moveSpeed * Time.deltaTime);
            TiltTowardsDirection(direction);
            hasFiredAtPoint = false;
        }
        else if (!hasFiredAtPoint)
        {
            StartCoroutine(PerformBurstFire());
        }
        else
        {
            SwayAtPoint();
        }

        lastPosition = transform.position;
    }

    private void TiltTowardsDirection(Vector3 direction)
    {
        if (direction != Vector3.zero)
        {
            float angleX = -direction.y * tiltAmount;
            float angleY = direction.x * tiltAmount;
            transform.rotation = Quaternion.Euler(angleX, angleY, 0);
        }
    }

    private void SwayAtPoint()
    {
        float swayY = Mathf.Sin(Time.time * swaySpeed + swayOffset) * swayHeight;
        transform.position = new Vector3(transform.position.x, patrolPoints[currentPointIndex].position.y + swayY, transform.position.z);
    }

    private IEnumerator PerformBurstFire()
    {
        isFiring = true;
        hasFiredAtPoint = true;

        yield return new WaitForSeconds(waitAtPointDuration);

        for (int i = 0; i < burstCount; i++)
        {
            for (int j = 0; j < bulletsPerBurst; j++)
            {
                float randomYOffset = Random.Range(offsetMin, offsetMax);
                Vector3 targetPos = player.position + new Vector3(0, randomYOffset, 0);
                var bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
                bullet.SetTarget(targetPos);
            }

            if (i < burstCount - 1)
                yield return new WaitForSeconds(burstInterval);
        }

        yield return new WaitForSeconds(fireCooldown);

        currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
        isFiring = false;
    }

    public void ReactToHit()
    {
        StartCoroutine(FreezeMovement(_frozeDuration));
    }

    private IEnumerator FreezeMovement(float duration)
    {
        isFrozen = true;
        yield return new WaitForSeconds(duration);
        isFrozen = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        foreach (Transform point in patrolPoints)
        {
            if (point != null)
                Gizmos.DrawSphere(point.position, 0.2f);
        }
    }
}
