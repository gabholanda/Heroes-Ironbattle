using UnityEngine;
public class GoblinPatrollingState : BaseState
{
    private readonly GoblinStateMachine _sm;

    private float checkRepathing;
    private readonly float checkRepathingTimer = 1f;

    private float idleTime;
    private float intervalToGoIdle;

    public GoblinPatrollingState(GoblinStateMachine stateMachine) : base("Patrol", stateMachine)
    {
        _sm = stateMachine;
    }
    public override void Enter()
    {
        _sm.actions.seekerAI.UpdatePath(_sm.actions.physics.rb);
        _sm.actions.graphics.PlayChildrenAnimations("Walk");
        idleTime = 0f;
        intervalToGoIdle = Random.Range(7f, 10f);
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
