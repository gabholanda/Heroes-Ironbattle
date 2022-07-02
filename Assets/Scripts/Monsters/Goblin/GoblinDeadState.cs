using UnityEngine;

public class GoblinDeadState : BaseState
{
    private readonly GoblinStateMachine _sm;
    private float fade = 1f;

    public GoblinDeadState(GoblinStateMachine stateMachine) : base("Dead", stateMachine)
    {
        _sm = stateMachine;
    }

    public override void UpdateLogic()
    {
        fade -= Time.deltaTime * 0.7f;
        _sm.actions.graphics.material.SetFloat("_Fade", fade);
        if (fade <= 0)
        {
            _sm.CleanDead();
        }
    }

}
