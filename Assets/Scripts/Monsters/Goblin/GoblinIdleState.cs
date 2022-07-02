using System.Collections;
using UnityEngine;

public class GoblinIdleState : BaseState
{
    private GoblinStateMachine _sm;

    public GoblinIdleState(GoblinStateMachine stateMachine) : base("Idle", stateMachine)
    {
        _sm = stateMachine;
    }

    public override void Enter()
    {
        _sm.actions.graphics.anim.Play("Idle");
        _sm.actions.graphics.PlayChildrenAnimations("Idle");
        _sm.StartCoroutine(Patrol());
    }

    public IEnumerator Patrol()
    {
        yield return new WaitForSeconds(1f);
        _sm.ChangeState(_sm.patrollingState);
    }
}
