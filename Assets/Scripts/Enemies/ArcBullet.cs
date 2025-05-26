using UnityEngine;

public class ArcBullet : MonoBehaviour
{
    public float travelTime = 1f;
    public float arcHeight = 2f;
    [SerializeField] float _rotateSpeed = 15f;

    private Vector3 startPoint;
    private Vector3 endPoint;
    private float elapsedTime;

    private bool launched = false;

    public void Launch(Vector3 targetPos)
    {
        startPoint = transform.position;
        endPoint = targetPos;
        launched = true;
    }

    void Update()
    {
        if (!launched) return;

        elapsedTime += Time.deltaTime;
        float t = Mathf.Clamp01(elapsedTime / travelTime);

        Vector3 linearPos = Vector3.Lerp(startPoint, endPoint, t);

        float arc = arcHeight * Mathf.Sin(t * Mathf.PI);
        transform.position = new Vector3(linearPos.x, linearPos.y + arc, linearPos.z);

        if (t >= 1f)
        {
            Destroy(gameObject);
        }

        transform.Rotate(0, 0, _rotateSpeed * Time.deltaTime);
    }
}
