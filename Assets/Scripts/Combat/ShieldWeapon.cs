using UnityEngine;

public class ShieldWeapon : MeleeWeapon
{
    public override void GatherInput()
    {
        bool _input = Input.GetMouseButton(1) || Input.GetButton("Fire2");

        if (_input)
        {
            EnableCollision();
        }
        else
        {
            DisableCollision();
        }
    }
}
