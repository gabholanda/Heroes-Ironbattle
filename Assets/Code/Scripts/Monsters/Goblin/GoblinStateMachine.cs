using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinStateMachine : StateMachine
{
    [HideInInspector]
    public StandardIdleState idleState;
    [HideInInspector]
    public StandardPatrollingState patrollingState;
    [HideInInspector]
    public StandardChasingState chasingState;
    [HideInInspector]
    public StandardAttackState attackState;
    [HideInInspector]
    public StandardDyingState dyingState;
    [HideInInspector]
    public StandardDeadState deadState;

    public MonsterActions actions;

    public Transform HitPoint;

    private void Awake()
    {
        actions = GetComponent<MonsterActions>();
        stats = new CharacterStats();
        stats.SetCharacterStats(baseStats);
        CreateStateDictionary();
        InstantiateDefaultStates();
        AddDefaultStates();
        currentState = GetInitialState();
        Invoke(nameof(EnterInitialState), 0.5f);
    }

    private void EnterInitialState()
    {
        currentState.Enter();
    }

    protected override BaseState GetInitialState()
    {
        return patrollingState;
    }

    private void InstantiateDefaultStates()
    {
        idleState = new StandardIdleState(this);
        patrollingState = new StandardPatrollingState(this);
        chasingState = new StandardChasingState(this);
        attackState = new StandardAttackState(this);
        dyingState = new StandardDyingState(this);
        deadState = new StandardDeadState(this);
    }

    private void AddDefaultStates()
    {
        AddState(idleState);
        AddState(patrollingState);
        AddState(chasingState);
        AddState(dyingState);
        AddState(deadState);
        AddState(attackState);
    }

    public void ChaseEnemy(Collider2D collider)
    {
        if (actions.target == null && !isDead)
        {
            actions.target = collider.transform;
            ChangeState(chasingState);
        }
    }

    public void AttackEnemy(Collider2D collider)
    {
        if (!isDead)
            ChangeState(attackState);
    }
}
