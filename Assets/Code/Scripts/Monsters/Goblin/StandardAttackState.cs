using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardAttackState : BaseState
{
    private readonly GoblinStateMachine _sm;

    public StandardAttackState(GoblinStateMachine stateMachine) : base("Attack", stateMachine)
    {
        _sm = stateMachine;
    }


    public override void Enter()
    {
        _sm.actions.graphics.PlayChildrenAnimations("Attack");
        ExecuteAbility();
        _sm.StartCoroutine(GoBackChasing());
    }

    public void ExecuteAbility()
    {
        if (_sm.actions.abilities[0].isCoolingDown)
        {
            _sm.ChangeState(_sm.chasingState);
        }
        _sm.actions.abilities[0].Execute(_sm.gameObject, new Vector2());
        _sm.actions.abilities[0].StartCooldown();
    }

    public IEnumerator GoBackChasing()
    {
        yield return new WaitForSeconds(0.4f);
        _sm.ChangeState(_sm.chasingState);
    }
}
