using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour, IReactOnHit
{
    [SerializeField] SideFlipper _flipper = null;
    [SerializeField] Bullet _bulletPrefab = null;
    [SerializeField] Animator _anim = null;
    public Transform firePoint;
    public float shootingRate = 1f;
    private float shootingCooldown;

    [Header("// FREEZE")]
    [SerializeField] float _frozeDuration = 0.3f;
    [SerializeField] bool isFrozen = false;

    [SerializeField] float detectionRange = 10f;

    private Transform playerTransform;

    private void Awake()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            playerTransform = playerObj.transform;
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

    private void Update()
    {
        if (isFrozen) return;
        if (IsPlayerInRange())
        {
            HandleShooting();
        }
    }

    private bool IsPlayerInRange()
    {
        if (playerTransform == null) return false;
        float distance = Vector3.Distance(transform.position, playerTransform.position);
        return distance <= detectionRange;
    }

    private void HandleShooting()
    {
        if (shootingCooldown <= 0f)
        {
            _anim.SetTrigger("Shoot");
            SpawnBullet();
            shootingCooldown = shootingRate;
        }

        shootingCooldown -= Time.deltaTime;
    }

    private void SpawnBullet()
    {
        if (_bulletPrefab != null && firePoint != null)
        {
            var _bulletInstance = Instantiate(_bulletPrefab, firePoint.position, Quaternion.identity);

            float offset = _flipper.IsFacingRight() ? 10f : -10f;
            Vector3 target = firePoint.position + new Vector3(offset, 0f, 0f);
            _bulletInstance.SetTarget(target);
        }
    }
}