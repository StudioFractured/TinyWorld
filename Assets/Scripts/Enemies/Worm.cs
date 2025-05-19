using System.Collections;
using UnityEngine;

public class Worm : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 2f;           // Movement speed
    public float offset = 3f;          // Range to move left and right from starting point

    private Vector2 startPos;          // Initial position
    private bool movingRight = true;   // Direction flag

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float targetX = movingRight ? startPos.x + offset : startPos.x - offset;

        // Move towards targetX
        Vector3 targetPos = new Vector3(targetX, transform.position.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        // Check if reached target, then flip direction
        if (Vector3.Distance(transform.position, targetPos) < 0.01f)
        {
            movingRight = !movingRight;

            // Flip the sprite or object visually
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }
}
