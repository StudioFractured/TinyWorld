using UnityEngine;

public class FlyingEnemy : MonoBehaviour
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

    [Header("Attack")]
    public float attackCooldown = 2f;

    private bool isAttacking = false;
    private bool onCooldown = false;
    private float cooldownTimer = 0f;

    private float hoverOffset;

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
    }

    private void Update()
    {
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
            ReturnToBaseWithHover();
        }
    }

    private void MoveTowardPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Vector3 targetPosition = player.position + direction * overshootDistance;

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, player.position) < 1f && !onCooldown)
        {
            Attack();
        }
    }

    private void ReturnToBaseWithHover()
    {
        Vector3 baseHoverPos = new Vector3(
            basePoint.position.x,
            basePoint.position.y + Mathf.Sin(Time.time * hoverSpeed + hoverOffset) * hoverHeight,
            basePoint.position.z
        );

        transform.position = Vector3.MoveTowards(transform.position, baseHoverPos, speed * Time.deltaTime);
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
    }
}
