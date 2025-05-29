using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
    [SerializeField] TagCollectionSO _tags = null;
    [SerializeField] UnityEvent OnTrigger = null;

    private void OnTriggerEnter2D(Collider2D _other)
    {
        if (!_tags.HasTag(_other.gameObject)) return;

        OnTrigger?.Invoke();
    }
}
