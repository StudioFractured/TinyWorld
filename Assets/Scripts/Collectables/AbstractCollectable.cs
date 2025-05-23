using UnityEngine;

public abstract class AbstractCollectable : MonoBehaviour
{
    [SerializeField] TagCollectionSO _tags = null;

    private void OnCollisionEnter2D(Collision2D _other)
    {

    }

    private void OnTriggerEnter2D(Collider2D _other)
    {
        if (_tags.HasTag(_other.gameObject))
        {
            OnCollect(_other);
        }
    }

    public abstract void OnCollect(Collider2D _colliderHit);
}
