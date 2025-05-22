using UnityEngine;
using System.Collections;

public class EnemyCharger : MonoBehaviour, IReactOnHit
{
    [Header("Patrol Settings")]
    public float patrolSpeed = 2f;
    public float patrolOffset = 3f;

    [Header("Detection Settings")]
    public float detectionRange = 5f;
    public LayerMask playerLayer;
    public string playerTag = "Player";

    [Header("Charge & Attack Settings")]
    public float chargeDuration = 1f;
    public float raycastLength = 10f;
    public float dashSpeed = 15f;
    public float dashDuration = 0.3f;
    public float attackCooldown = 2f;

    [Header("Dash FX (Optional)")]
    public bool flipOnTurn = true;

    private Vector2 startPosition;
    private bool movingRight = true;
    private bool canAttack = true;
    private bool isDashing = false;

    private Rigidbody2D rb;
    private Vector2 dashDirection;
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
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
    }

    private void Update()
    {
        if (isFrozen) return;
        if (isDashing) return;

        GameObject player = GameObject.FindWithTag(playerTag);

        if (player != null && Vector2.Distance(transform.position, player.transform.position) <= detectionRange)
        {
            if (canAttack)
                StartCoroutine(ChargeAndDash(player.transform));
        }
        else
        {
            Patrol();
        }
    }

    private void Patrol()
    {
        float targetX = movingRight ? startPosition.x + patrolOffset : startPosition.x - patrolOffset;
        Vector2 target = new Vector2(targetX, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, target, patrolSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, target) < 0.1f)
        {
            movingRight = !movingRight;
            if (flipOnTurn)
                Flip();
        }
    }

    private IEnumerator ChargeAndDash(Transform player)
    {
        canAttack = false;

        float firstWait = chargeDuration * 0.33f;
        float secondWait = chargeDuration * 0.67f;

        yield return new WaitForSeconds(firstWait);

        Vector2 direction = (player.position - transform.position).normalized;
        dashDirection = new Vector2(Mathf.Sign(direction.x), 0f);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dashDirection, raycastLength, playerLayer);

        Debug.DrawRay(transform.position, dashDirection * raycastLength, Color.red, 1f);

        if (hit.collider == null || !hit.collider.CompareTag(playerTag))
        {
            Debug.Log("Charge canceled - player not in path");
            canAttack = true;
            yield break;
        }

        Debug.Log("Player detected in dash path");

        yield return new WaitForSeconds(secondWait);

        StartCoroutine(PerformDash());

        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    private IEnumerator PerformDash()
    {
        isDashing = true;

        float timer = 0f;
        while (timer < dashDuration)
        {
            rb.linearVelocity = new Vector2(dashDirection.x * dashSpeed, rb.linearVelocity.y);
            timer += Time.deltaTime;
            yield return null;
        }

        rb.linearVelocity = Vector2.zero;
        isDashing = false;
    }

    private void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * raycastLength);
        Gizmos.DrawLine(transform.position, transform.position + Vector3.left * raycastLength);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
