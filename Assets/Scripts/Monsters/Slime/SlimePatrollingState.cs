using UnityEngine;

public class SlimePatrollingState : BaseState
{
    private readonly SlimeStateMachine _sm;

    private float checkRepathing;
    private readonly float checkRepathingTimer = 1f;
    private readonly float upSpeed = 1f;
    private readonly float downSpeed = 1.6f;

    public SlimePatrollingState(SlimeStateMachine stateMachine) : base("Patrol", stateMachine)
    {
        _sm = stateMachine;
    }
    public override void Enter()
    {
        _sm.actions.seekerAI.UpdatePath(_sm.actions.physics.rb);
    }

    public override void UpdateLogic()
    {
        _sm.actions.seekerAI.UpdatePath(_sm.actions.physics.rb);
        if (_sm.goUp)
            _sm.GoUp(upSpeed);
        else
            _sm.GoDown(downSpeed);

        _sm.actions.CanRepath(ref checkRepathing, checkRepathingTimer);
    }

    public override void UpdatePhysics()
    {
        _sm.actions.SetMovement(_sm.stats.MoveSpeed);
    }

    public override void Exit()
    {
        _sm.actions.seekerAI.ResetWayPoint();
    }




}
