using System.Collections;
using UnityEngine;

public class Worm : MonoBehaviour, IReactOnHit
{
    [Header("Movement")]
    public float speed = 2f;           // Movement speed
    public float offset = 3f;          // Range to move left and right from starting point

    private Vector2 startPos;          // Initial position
    private bool movingRight = true;   // Direction flag

    [Header("// FREEZE")]
    [SerializeField] float _frozeDuration = 0.3f;
    [SerializeField] bool isFrozen = false;

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

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        if (isFrozen) return;
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
