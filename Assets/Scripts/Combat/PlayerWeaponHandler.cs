using System.Collections;
using UnityEngine;

public class PlayerWeaponHandler : MonoBehaviour
{
    [SerializeField] GameObject _sword = null;
    [SerializeField] float _time = 0.5f;

    private void Awake()
    {
        _sword.SetActive(false);
    }

    private void Update()
    {
        if (_sword.activeInHierarchy) return;

        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(Sword_Routine());
        }
    }

    private IEnumerator Sword_Routine()
    {
        _sword.SetActive(true);
        yield return new WaitForSeconds(_time);
        _sword.SetActive(false);
    }
}
