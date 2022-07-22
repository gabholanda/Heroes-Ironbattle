using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinStateMachine : StateMachine
{
    [HideInInspector]
    public GoblinIdleState idleState;
    [HideInInspector]
    public GoblinPatrollingState patrollingState;
    [HideInInspector]
    public GoblinChasingState chasingState;
    [HideInInspector]
    public GoblinAttackState attackState;
    [HideInInspector]
    public GoblinDyingState dyingState;
    [HideInInspector]
    public GoblinDeadState deadState;

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
        idleState = new GoblinIdleState(this);
        patrollingState = new GoblinPatrollingState(this);
        chasingState = new GoblinChasingState(this);
        attackState = new GoblinAttackState(this);
        dyingState = new GoblinDyingState(this);
        deadState = new GoblinDeadState(this);
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
