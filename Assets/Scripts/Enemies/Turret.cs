using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour, IReactOnHit
{
    [SerializeField] SideFlipper _flipper = null;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float shootingRate = 1f; // seconds between shots
    //public bool shootRight = true;  // Set to false to shoot left

    private float shootingCooldown;
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

    private void Update()
    {
        if (isFrozen) return;
        HandleShooting();
    }

    private void HandleShooting()
    {
        if (shootingCooldown <= 0f)
        {
            SpawnBullet();
            shootingCooldown = shootingRate;
        }

        shootingCooldown -= Time.deltaTime;
    }

    private void SpawnBullet()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

            KnightSlash bulletScript = bullet.GetComponent<KnightSlash>();
            if (bulletScript != null)
            {
                // Shoot far to the right or left so Mathf.Sign can detect direction
                //float offset = shootRight ? 10f : -10f;
                float offset = _flipper.IsFacingRight() ? 10f : -10f;
                Vector3 target = firePoint.position + new Vector3(offset, 0f, 0f);
                bulletScript.SetTarget(target);
            }
        }
    }
}
