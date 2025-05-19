using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "Condition_AlwaysFalse", menuName = "SO/State Machines/Conditions/Always False")]
public class AlwaysFalseConditionSO : StateConditionSO<AlwaysFalseCondition>
{
    //
}

public class AlwaysFalseCondition : Condition
{
    protected override bool Statement()
    {
        return false;
    }
}
