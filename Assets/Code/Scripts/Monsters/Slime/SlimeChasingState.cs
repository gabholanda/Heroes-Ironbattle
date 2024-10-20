using UnityEngine;

public class SlimeChasingState : BaseState
{
    private readonly SlimeStateMachine _sm;

    private float checkRepathing = 0f;
    private readonly float checkRepathingTimer = 0.3f;
    private readonly float upSpeed = 1f;
    private readonly float downSpeed = 1.6f;

    public SlimeChasingState(SlimeStateMachine stateMachine) : base("Chasing", stateMachine)
    {
        _sm = stateMachine;
    }

    public override void Enter()
    {
        _sm.actions.graphics.anim.Play("Walk");
        _sm.actions.SetRepeat(0, .5f, "SeekTarget");
    }

    public override void UpdateLogic()
    {
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
        _sm.actions.StopRepeat("SeekTarget");
    }
}
