using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeDeadState : BaseState
{
    private SlimeStateMachine _sm;
    private float fade = 1f;

    public SlimeDeadState(SlimeStateMachine stateMachine) : base("Dead", stateMachine)
    {
        _sm = stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        _sm.anim.Play("Dead");
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        fade -= Time.deltaTime * 0.7f;
        _sm.material.SetFloat("_Fade", fade);
        if (fade <= 0)
        {
            _sm.CleanDead();
        }
    }

}
