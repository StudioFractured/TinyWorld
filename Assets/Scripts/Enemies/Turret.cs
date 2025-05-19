using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject bulletPrefab; 
    public Transform firePoint;     
    public float detectionRange = 10f;
    public float shootingRate = 1f; 
    public GameObject player;        

    private bool isPlayerInRange = false;
    private float shootingCooldown;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        DetectPlayer();
        HandleShooting();
    }

    private void DetectPlayer()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= detectionRange)
        {
            isPlayerInRange = true;
        }
        else
        {
            isPlayerInRange = false;
        }
    }

    private void HandleShooting()
    {
        if (isPlayerInRange)
        {
            if (shootingCooldown <= 0f)
            {
                SpawnBullet(player.transform.position);
                shootingCooldown = shootingRate;
            }
        }

        if (shootingCooldown > 0f)
        {
            shootingCooldown -= Time.deltaTime;
        }
    }

    private void SpawnBullet(Vector3 targetPosition)
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.SetTarget(targetPosition);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
