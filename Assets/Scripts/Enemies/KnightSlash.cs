using UnityEngine;

public class KnightSlash : MonoBehaviour
{
    public float speed = 10f;
    private Vector3 direction;

    public void SetTarget(Vector3 target)
    {
        float dirX = Mathf.Sign(target.x - transform.position.x);
        direction = new Vector3(dirX, 0f, 0f);
        Destroy(gameObject, 10f);
    }

    private void Update()
    {
        transform.position += speed * Time.deltaTime * direction;
    }
}
