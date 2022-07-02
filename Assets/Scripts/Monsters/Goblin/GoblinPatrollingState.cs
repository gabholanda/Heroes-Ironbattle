public class GoblinPatrollingState : BaseState
{
    private readonly GoblinStateMachine _sm;

    private float checkRepathing;
    private readonly float checkRepathingTimer = 1f;

    public GoblinPatrollingState(GoblinStateMachine stateMachine) : base("Patrol", stateMachine)
    {
        _sm = stateMachine;
    }
    public override void Enter()
    {
        _sm.actions.seekerAI.UpdatePath(_sm.actions.physics.rb);
        _sm.actions.graphics.anim.Play("Walk");
        _sm.actions.graphics.PlayChildrenAnimations("Walk");
    }

    public override void UpdateLogic()
    {
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
