using System.Collections;
using UnityEngine;

public class Thrower : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 2f;
    public float offset = 3f;

    [Header("Detection & Attack")]
    public float detectionRange = 5f;
    public float stopDuration = 1f;
    public float attackCooldown = 2f;
    public GameObject arcBulletPrefab;
    public Transform firePoint;
    public string playerTag = "Player";

    private Vector2 startPos;
    private bool movingRight = true;
    private bool canAttack = true;
    private Transform player;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag(playerTag)?.transform;

        if (player != null && Vector2.Distance(transform.position, player.position) <= detectionRange)
        {
            if (canAttack)
                StartCoroutine(ShootRoutine());
            return;
        }

        Patrol();
    }

    private void Patrol()
    {
        float targetX = movingRight ? startPos.x + offset : startPos.x - offset;
        Vector3 targetPos = new Vector3(targetX, transform.position.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPos) < 0.01f)
        {
            movingRight = !movingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }

    private IEnumerator ShootRoutine()
    {
        canAttack = false;

        yield return new WaitForSeconds(stopDuration);

        if (arcBulletPrefab && player)
        {
            GameObject bullet = Instantiate(arcBulletPrefab, firePoint.position, Quaternion.identity);
            bullet.GetComponent<ArcBullet>().Launch(player.position);
        }

        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
