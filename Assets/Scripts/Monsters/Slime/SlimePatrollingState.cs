using UnityEngine;

public class SlimePatrollingState : BaseState
{
    private readonly SlimeStateMachine _sm;

    private float checkRepathing;
    private readonly float checkRepathingTimer = 1f;
    private readonly float upSpeed = 1f;
    private readonly float downSpeed = 1.6f;

    private float idleTime;
    private float intervalToGoIdle;

    public SlimePatrollingState(SlimeStateMachine stateMachine) : base("Patrol", stateMachine)
    {
        _sm = stateMachine;
    }
    public override void Enter()
    {
        _sm.actions.seekerAI.UpdatePath(_sm.actions.physics.rb);
        idleTime = 0f;
        intervalToGoIdle = Random.Range(6f, 10f);
        _sm.actions.graphics.anim.Play("Walk");
    }

    public override void UpdateLogic()
    {
        idleTime += Time.deltaTime;
        if (idleTime > intervalToGoIdle)
        {
            _sm.ChangeState(_sm.idleState);
            return;
        }
        _sm.actions.seekerAI.UpdatePath(_sm.actions.physics.rb);
        if (_sm.goUp)
            _sm.GoUp(upSpeed);
        else
            _sm.GoDown(downSpeed);

        _sm.actions.CanRepath(ref checkRepathing, checkRepathingTimer);
    }

    public override void UpdatePhysics()
    {
        _sm.actions.SetMovement(_sm.stats.combatStats.MoveSpeed);
    }

    public override void Exit()
    {
        _sm.actions.seekerAI.ResetWayPoint();
    }

}
