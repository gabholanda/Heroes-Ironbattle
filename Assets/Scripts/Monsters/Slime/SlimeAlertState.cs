using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAlertState : BaseState
{
    private SlimeStateMachine _sm;
    private float changeStateTimer = 0.5f;
    private float changeStateDur;
    public SlimeAlertState(SlimeStateMachine stateMachine) : base("Alert", stateMachine)
    {
        _sm = stateMachine;
    }

    public override void Enter()
    {
        _sm.anim.Play("Alert");
        _sm.ResetWayPoint();
    }

    public override void UpdateLogic()
    {
        changeStateDur += Time.deltaTime;
        if (changeStateDur > changeStateTimer)
            _sm.ChangeState(_sm.chasingState);
    }
}
