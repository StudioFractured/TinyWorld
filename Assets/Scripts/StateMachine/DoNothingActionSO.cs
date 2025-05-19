using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "Action_DoNothing", menuName = "SO/State Machines/Actions/Do Nothing")]
public class DoNothingActionSO : StateActionSO<DoNothingAction>
{
    //
}

public class DoNothingAction : StateAction
{
    public override void OnFixedUpdate()
    {
        //
    }

    public override void OnUpdate()
    {
        //
    }
}
