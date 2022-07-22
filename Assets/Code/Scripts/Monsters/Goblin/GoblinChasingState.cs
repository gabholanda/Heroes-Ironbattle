public class GoblinChasingState : BaseState
{
    private readonly GoblinStateMachine _sm;

    private float checkRepathing = 0f;
    private readonly float checkRepathingTimer = 0.3f;


    public GoblinChasingState(GoblinStateMachine stateMachine) : base("Chasing", stateMachine)
    {
        _sm = stateMachine;
    }

    public override void Enter()
    {
        _sm.actions.graphics.PlayChildrenAnimations("Walk");
        _sm.actions.SetRepeat(0, .5f, "SeekTarget");
    }

    public override void UpdateLogic()
    {
        _sm.actions.CanRepath(ref checkRepathing, checkRepathingTimer);
    }

    public override void UpdatePhysics()
    {
        _sm.actions.SetMovement(_sm.stats.combatStats.MoveSpeed);
    }

    public override void Exit()
    {
        _sm.actions.seekerAI.ResetWayPoint();
        _sm.actions.StopRepeat("SeekTarget");
    }
}
