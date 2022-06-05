using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAttackState : BaseState
{
    private SlimeStateMachine _sm;
    private float upSpeed = 1.4f;
    private float downSpeed = 2.3f;
    private float totalTime;
    public SlimeAttackState(SlimeStateMachine stateMachine) : base("Attack", stateMachine)
    {
        _sm = stateMachine;
    }

    public override void Enter()
    {
        _sm.anim.Play("Attack");
        ExecuteAbility();
        AddForceTowardsTarget();
        _sm.StartCoroutine(AttackTime());
    }

    public override void UpdatePhysics()
    {
        if (_sm.goUp)
            _sm.GoUp(upSpeed);
        else
            _sm.GoDown(downSpeed);
    }

    public override void Exit()
    {
        _sm.ResetWayPoint();
        //Physics2D.IgnoreCollision(_sm.col, _sm.target.GetComponent<Collider2D>(), false);
    }

    private void ExecuteAbility()
    {
        _sm.ability.Execute(_sm.gameObject, _sm.transform.position);
        totalTime = _sm.goUpTime + _sm.goDownTime;
        _sm.ability.GetAbilityData().cooldownDuration = totalTime;
    }

    private void AddForceTowardsTarget()
    {
        float speed = _sm.stats.combatStats.MoveSpeed * 600f;
        Vector2 direction = ((Vector2)_sm.target.transform.position - _sm.rb.position).normalized;
        Vector2 force = direction * speed;
        _sm.rb.AddForce(force);
        //Physics2D.IgnoreCollision(_sm.col, _sm.target.GetComponent<Collider2D>());
    }

    private IEnumerator AttackTime()
    {
        yield return new WaitForSeconds(totalTime);
        if (!_sm.isDead)
            _sm.ChangeState(_sm.chasingState);
    }
}
