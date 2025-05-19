using UnityEngine;

public class PlayerWeaponHandler : MonoBehaviour
{
    [SerializeField] MeleeWeapon _standSword = null;
    [SerializeField] MeleeWeapon _crouchSword = null;

    public MeleeWeapon StandSword { get => _standSword; }
    public MeleeWeapon CrouchSword { get => _crouchSword; }

    public bool IsAttacking()
    {
        return _standSword.IsAttacking() || _crouchSword.IsAttacking();
    }
}
