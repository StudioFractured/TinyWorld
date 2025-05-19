using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    private GameObject bullet;
    private Vector3 direction;

    public void SetTarget(Vector3 target)
    {
        direction = (target - transform.position).normalized;
        Destroy(bullet, 10);
    }

    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
