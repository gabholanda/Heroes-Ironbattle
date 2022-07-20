using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeIdleState : BaseState
{
    private SlimeStateMachine _sm;

    private float patrollingTime;
    private float intervalToGoPatrol;

    public SlimeIdleState(SlimeStateMachine stateMachine) : base("Idle", stateMachine)
    {
        _sm = stateMachine;
    }

    public override void Enter()
    {
        patrollingTime = 0f;
        intervalToGoPatrol = Random.Range(1f, 5f);
        _sm.actions.graphics.anim.Play("Idle");
    }

    public override void UpdateLogic()
    {
        patrollingTime += Time.deltaTime;
        if (patrollingTime > intervalToGoPatrol)
        {
            _sm.ChangeState(_sm.patrollingState);
            return;
        }
    }
}