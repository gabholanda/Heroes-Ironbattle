using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAttackState : BaseState
{
    private SlimeStateMachine _sm;
    private float upSpeed = 1.4f;
    private float downSpeed = 2.3f;
    private bool finishedAttack;

    public SlimeAttackState(SlimeStateMachine stateMachine) : base("Attack", stateMachine)
    {
        _sm = stateMachine;
    }

    public override void Enter()
    {
        _sm.anim.Play("Attack");
        _sm.SetRepeat(0, 1.5f, "CheckAttack");
    }

    public override void UpdatePhysics()
    {
        _sm.SetMovement(_sm.stats.moveSpeed * 1.5f);
        if (_sm.goUp)
            _sm.GoUp(upSpeed);
        else
            _sm.GoDown(downSpeed);
    }

    public override void Exit()
    {
        _sm.ResetWayPoint();
    }

}
