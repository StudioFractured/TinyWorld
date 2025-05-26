using UnityEngine;
using System.Collections;

public class Knight : MonoBehaviour, IReactOnHit
{
    [Header("Patrol Settings")]
    public float patrolSpeed = 2f;
    public float patrolOffsetMin = 2f;
    public float patrolOffsetMax = 5f;

    [Header("Detection Settings")]
    public float detectionRange = 5f;
    public LayerMask playerLayer;
    public string playerTag = "Player";
    private Transform player;

    [Header("Charge & Attack Settings")]
    public Bullet bulletPrefab;
    public Transform slashPoint;
    public float chargeDuration = 1f;
    public float raycastLength = 10f;
    public float dashSpeed = 15f;
    public float dashDuration = 0.3f;
    public float attackCooldown = 2f;

    [Header("Raycast Settings")]
    public int rayCount = 5;
    public float angleSpread = 20f;
    public float raycastYOffset = 0f;

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
        player = GameObject.FindGameObjectWithTag(playerTag)?.transform;

        // Randomize patrol offset at start
        float randomOffset = Random.Range(patrolOffsetMin, patrolOffsetMax);
        patrolOffsetMin = patrolOffsetMax = randomOffset;
    }

    private void Update()
    {
        if (isFrozen) return;
        if (isDashing) return;

        if (player == null) return;

        if (Vector2.Distance(transform.position, player.position) <= detectionRange)
        {
            if (canAttack)
                StartCoroutine(ChargeAndAttack(player.transform));
        }
        else
        {
            Patrol();
        }
    }

    private void Patrol()
    {
        float targetX = movingRight ? startPosition.x + patrolOffsetMax : startPosition.x - patrolOffsetMax;
        Vector2 target = new Vector2(targetX, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, target, patrolSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, target) < 0.1f)
        {
            movingRight = !movingRight;
            //if (flipOnTurn)
            //    Flip();
        }
    }

    private IEnumerator ChargeAndAttack(Transform player)
    {
        canAttack = false;

        float firstWait = chargeDuration * 0.33f;
        float secondWait = chargeDuration * 0.67f;

        yield return new WaitForSeconds(firstWait);

        Vector2 baseDirection = new Vector2(Mathf.Sign(player.position.x - transform.position.x), 0f);
        dashDirection = baseDirection;

        bool playerDetected = false;
        Vector2 origin = new Vector2(transform.position.x, transform.position.y + raycastYOffset);

        for (int i = 0; i < rayCount; i++)
        {
            float t = (float)i / (rayCount - 1);
            float angleOffset = Mathf.Lerp(-angleSpread / 2f, angleSpread / 2f, t);
            Vector2 rayDir = Quaternion.Euler(0, 0, angleOffset) * baseDirection;

            RaycastHit2D hit = Physics2D.Raycast(origin, rayDir, raycastLength, playerLayer);
            Debug.DrawRay(origin, rayDir * raycastLength, Color.red, 1f);

            if (hit.collider != null && hit.collider.CompareTag(playerTag))
            {
                playerDetected = true;
                break;
            }
        }

        if (!playerDetected)
        {
            Debug.Log("Charge canceled - player not in ray path");
            canAttack = true;
            yield break;
        }

        Debug.Log("Player detected - charging...");

        yield return new WaitForSeconds(secondWait);
        StartCoroutine(Attack());

        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    private IEnumerator Attack()
    {
        if (bulletPrefab != null)
        {
            //Vector3 spawnPosition = new (slashPoint.position.x, -2f, 0);
            var _bulletInstance = Instantiate(bulletPrefab, slashPoint.position, Quaternion.identity);
            _bulletInstance.SetTarget(new Vector2(dashDirection.x, 0f));
        }

        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }


    private void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void OnDrawGizmosSelected()
    {
        Vector2 origin = new Vector2(transform.position.x, transform.position.y + raycastYOffset);

        Gizmos.color = Color.red;
        Vector2 baseDirRight = Vector2.right;
        Vector2 baseDirLeft = Vector2.left;

        for (int i = 0; i < rayCount; i++)
        {
            float t = (float)i / (rayCount - 1);
            float angleOffset = Mathf.Lerp(-angleSpread / 2f, angleSpread / 2f, t);

            Vector2 rayRight = Quaternion.Euler(0, 0, angleOffset) * baseDirRight;
            Gizmos.DrawLine(origin, origin + rayRight * raycastLength);

            Vector2 rayLeft = Quaternion.Euler(0, 0, angleOffset) * baseDirLeft;
            Gizmos.DrawLine(origin, origin + rayLeft * raycastLength);
        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
