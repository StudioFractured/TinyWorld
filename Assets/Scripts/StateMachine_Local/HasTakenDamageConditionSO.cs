using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "Condition_HasTakenDamage", menuName = "SO/State Machines/Conditions/Has Taken Damage")]
public class HasTakenDamageConditionSO : StateConditionSO<HasTakenDamageCondition>
{
    //
}

public class HasTakenDamageCondition : Condition
{
    private HealthBehaviour _health = null;

    public override void Awake(StateMachine _stateMachine)
    {
        _health = _stateMachine.GetComponent<HealthBehaviour>();
    }

    protected override bool Statement()
    {
        return _health.HasTakenDamageThisFrame;
    }
}
