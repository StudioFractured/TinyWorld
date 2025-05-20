using UnityEngine;

public class ShieldWeapon : MeleeWeapon
{
    public override void GatherInput()
    {
        if (_input.DefendPressed)
        {
            EnableCollision();
        }
        else
        {
            DisableCollision();
        }
    }
}
