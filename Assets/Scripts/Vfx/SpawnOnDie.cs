using UnityEngine;

public class SpawnOnDie : MonoBehaviour
{
    [SerializeField] HealthBehaviour _health = null;
    [SerializeField] GameObject _prefab = null;
    [SerializeField] Transform _spawnPoint = null;
    [SerializeField] Vector2 _offset = default;

    private void OnEnable()
    {
        _health.OnDie += Play;
    }

    private void OnDisable()
    {
        _health.OnDie -= Play;
    }

    public void Play()
    {
        Instantiate(_prefab, (Vector2)_spawnPoint.position + _offset, Quaternion.identity);
    }
}
