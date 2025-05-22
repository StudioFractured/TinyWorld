using UnityEngine;
using System.Collections;

public class FlyingEnemy : MonoBehaviour, IReactOnHit
{
    [Header("References")]
    public Transform basePoint;
    public Transform player;

    [Header("Movement")]
    public float speed = 5f;
    public float chaseRange = 10f;
    public float overshootDistance = 2f;

    [Header("Hovering")]
    public float hoverSpeed = 2f;
    public float hoverHeight = 0.5f;
    public float idleRange = 3f;
    public float idleTargetRefreshTime = 3f;

    [Header("Attack")]
    public float attackCooldown = 2f;

    [Header("// FREEZE")]
    [SerializeField] float _frozeDuration = 0.3f;
    [SerializeField] bool isFrozen = false;

    private bool onCooldown = false;
    private float cooldownTimer = 0f;

    private float hoverOffset;
    private Vector3 idleTarget;
    private float idleTimer;

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

        hoverOffset = Random.Range(0f, 2f * Mathf.PI);
        PickNewIdleTarget();
    }

    private void Update()
    {
        if (isFrozen) return;
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(player.position, basePoint.position);

        if (onCooldown)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0f)
                onCooldown = false;
        }

        if (distanceToPlayer <= chaseRange)
        {
            MoveTowardPlayer();
        }
        else
        {
            //IdleAroundBase();
        }
    }

    private void MoveTowardPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Vector3 targetPosition = player.position + direction * overshootDistance;

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        //if (Vector3.Distance(transform.position, player.position) < 1f && !onCooldown)
        //{
        //    Attack();
        //}
    }

    private void IdleAroundBase()
    {
        idleTimer -= Time.deltaTime;

        if (idleTimer <= 0f || Vector3.Distance(transform.position, idleTarget) < 0.2f)
        {
            PickNewIdleTarget();
        }

        // Add hover oscillation to the Y value
        float hoverY = Mathf.Sin(Time.time * hoverSpeed + hoverOffset) * hoverHeight;
        Vector3 hoverPos = new Vector3(idleTarget.x, basePoint.position.y + hoverY, idleTarget.z);

        transform.position = Vector3.MoveTowards(transform.position, hoverPos, speed * Time.deltaTime);
    }

    private void PickNewIdleTarget()
    {
        Vector2 randomCircle = Random.insideUnitCircle * idleRange;
        idleTarget = basePoint.position + new Vector3(randomCircle.x, 0, randomCircle.y);
        idleTimer = idleTargetRefreshTime;
    }

    private void Attack()
    {
        onCooldown = true;
        cooldownTimer = attackCooldown;

        Debug.Log("Enemy attacked!");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        if (basePoint != null)
            Gizmos.DrawWireSphere(basePoint.position, chaseRange);

        Gizmos.color = Color.yellow;
        if (basePoint != null)
            Gizmos.DrawWireSphere(basePoint.position, idleRange);
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
}
