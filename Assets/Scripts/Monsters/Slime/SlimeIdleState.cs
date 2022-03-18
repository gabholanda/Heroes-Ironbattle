using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeIdleState : BaseState
{
    private SlimeStateMachine _sm;

    public SlimeIdleState(SlimeStateMachine stateMachine) : base("Idle", stateMachine)
    {
        _sm = stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        stateMachine.ChangeState(_sm.patrollingState);
    }
}