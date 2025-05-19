using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "Condition_IsAttacking", menuName = "SO/State Machines/Conditions/Is Attacking")]
public class IsAttackingConditionSO : StateConditionSO<IsAttackingCondition>
{
    //
}

public class IsAttackingCondition : Condition
{
    private PlayerWeaponHandler _weaponHandler = null;

    public override void Awake(StateMachine _stateMachine)
    {
        _weaponHandler = _stateMachine.GetComponent<PlayerWeaponHandler>();
    }

    protected override bool Statement()
    {
        return _weaponHandler.IsAttacking();
    }
}
