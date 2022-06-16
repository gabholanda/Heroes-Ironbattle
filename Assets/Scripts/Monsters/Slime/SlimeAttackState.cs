using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAttackState : BaseState
{
    private readonly SlimeStateMachine _sm;
    private readonly float upSpeed = 1.4f;
    private readonly float downSpeed = 2.3f;
    private float totalTime;
    public SlimeAttackState(SlimeStateMachine stateMachine) : base("Attack", stateMachine)
    {
        _sm = stateMachine;
    }

    public override void Enter()
    {
        _sm.actions.graphics.anim.Play("Attack");
        ExecuteAbility();
        AddForceTowardsTarget();
        _sm.StartCoroutine(AttackTime());
    }

    public override void UpdateLogic()
    {
        if (_sm.goUp)
            _sm.GoUp(upSpeed);
        else
            _sm.GoDown(downSpeed);
    }

    public override void Exit()
    {
        _sm.actions.seekerAI.ResetWayPoint();
    }

    private void ExecuteAbility()
    {
        _sm.actions.abilities[0].Execute(_sm.gameObject, _sm.transform.position);
        totalTime = _sm.goUpTime + _sm.goDownTime;
        _sm.actions.abilities[0].GetAbilityData().cooldownDuration = totalTime;
    }

    private void AddForceTowardsTarget()
    {
        float speed = _sm.stats.combatStats.MoveSpeed * 600f;
        Vector2 direction = ((Vector2)_sm.actions.target.transform.position - _sm.actions.physics.rb.position).normalized;
        Vector2 force = direction * speed;
        _sm.actions.physics.rb.AddForce(force);
    }

    private IEnumerator AttackTime()
    {
        yield return new WaitForSeconds(totalTime);
        if (!_sm.isDead)
            _sm.ChangeState(_sm.chasingState);
    }
}
