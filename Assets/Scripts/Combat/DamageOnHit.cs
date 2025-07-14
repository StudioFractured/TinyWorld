using NUnit.Framework.Internal;
using System;
using System.Threading.Tasks;
using UnityEngine;

public class DamageOnHit : MonoBehaviour
{
    [SerializeField] TagCollectionSO _tags = null;
    [SerializeField] int _damage = 1;
    [SerializeField] bool _destroyOnHit = false;
    private Inventory inventory;

    private void Awake()
    {
        inventory = FindFirstObjectByType<Inventory>();
    }

    private void OnTriggerEnter2D(Collider2D _other)
    {
        if (!_tags.HasTag(_other.gameObject)) return;

        if (_other.TryGetComponent(out ChestContents _chest))
        {
            if(inventory.key == 0) { goto bigJump; }
            if (_chest.chestType == ChestTypes.Common)
            {
                Debug.Log("Common");
                _chest.OpenChest(_chest.gameObject, new Vector2(1f, 3f), new Vector2(2f, 4f), _chest.chestType);
            }
            else if (_chest.chestType == ChestTypes.Rare)
            {
                Debug.Log("Rare");
                _chest.OpenChest(gameObject, new Vector2(2f, 4f), new Vector2(3f, 6f), _chest.chestType);
            }
            else if (_chest.chestType == ChestTypes.Epic)
            {
                Debug.Log("Epic");
                _chest.OpenChest(gameObject, new Vector2(3f, 5f), new Vector2(4f, 8f), _chest.chestType);
            }
        }
    bigJump:

        if (_other.TryGetComponent(out HealthBehaviour _health))
        {
            _health.TakeDamage(gameObject, _damage);
        }

        if (_other.TryGetComponent<IReactOnHit>(out var reactable))
        {
            reactable.ReactToHit();
        }

        if (_destroyOnHit)
        {
            var _task = DestroyAsync();
            gameObject.SetActive(false);
        }
    }

    private async Task DestroyAsync()
    {
        await Task.Delay(1000);
        Destroy(gameObject);
    }

    public void IncreaseDamage()
    {
        _damage++;
    }
}
