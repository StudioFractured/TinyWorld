using System.Collections;
using UnityEngine;

public class PlayerWeaponHandler : MonoBehaviour
{
    [SerializeField] GameObject _sword = null;
    [SerializeField] float _time = 0.5f;

    private void Awake()
    {
        EndAttack();
    }

    private void Update()
    {
        GatherInput();
    }

    public void GatherInput()
    {
        if (IsAttacking()) return;

        if (Input.GetMouseButtonDown(0) || Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(Sword_Routine());
        }
    }

    private IEnumerator Sword_Routine()
    {
        StartAttack();
        yield return new WaitForSeconds(_time);
        EndAttack();
    }

    private void StartAttack()
    {
        _sword.SetActive(true);
    }

    private void EndAttack()
    {
        _sword.SetActive(false);
    }

    public bool IsAttacking()
    {
        return _sword.activeInHierarchy;
    }
}
