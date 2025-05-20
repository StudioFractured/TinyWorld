using UnityEngine;

public class SpawnOnDie : MonoBehaviour
{
    [SerializeField] HealthBehaviour _health = null;
    [SerializeField] GameObject _prefab = null;
    [SerializeField] Transform _spawnPoint = null;

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
        Instantiate(_prefab, _spawnPoint.position, Quaternion.identity);
    }
}
