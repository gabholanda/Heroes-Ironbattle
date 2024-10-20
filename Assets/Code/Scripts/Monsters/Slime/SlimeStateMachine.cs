using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class SlimeStateMachine : StateMachine
{
    [Header("States")]
    [HideInInspector]
    public SlimeIdleState idleState;
    [HideInInspector]
    public SlimePatrollingState patrollingState;
    [HideInInspector]
    public SlimeAlertState alertState;
    [HideInInspector]
    public SlimeChasingState chasingState;
    [HideInInspector]
    public SlimeAttackState attackState;
    [HideInInspector]
    public SlimeDyingState dyingState;
    [HideInInspector]
    public SlimeDeadState deadState;

    public MonsterActions actions;

    [Header("Jumping mechanics data")]
    public float goUpTime = 0.5f;
    public bool goUp = true;
    public float goDownTime = 0.3f;

    [SerializeField]
    private AudioSource jumpAudio;

    private void Awake()
    {
        jumpAudio = GetComponent<AudioSource>();
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
    private void InstantiateDefaultStates()
    {
        idleState = new SlimeIdleState(this);
        patrollingState = new SlimePatrollingState(this);
        alertState = new SlimeAlertState(this);
        chasingState = new SlimeChasingState(this);
        attackState = new SlimeAttackState(this);
        dyingState = new SlimeDyingState(this);
        deadState = new SlimeDeadState(this);
    }

    private void AddDefaultStates()
    {
        AddState(idleState);
        AddState(patrollingState);
        AddState(alertState);
        AddState(chasingState);
        AddState(dyingState);
        AddState(deadState);
        AddState(attackState);
    }
    protected override BaseState GetInitialState()
    {
        return patrollingState;
    }

    public void GoUp(float speed)
    {
        actions.graphics.gfxTransform.localPosition += new Vector3(
                0,
                speed * Time.deltaTime,
                0);
        if (actions.graphics.gfxTransform.localPosition.y > 0.5f)
        {
            goUp = false;
        }
    }

    public void GoDown(float speed)
    {
        actions.graphics.gfxTransform.localPosition -= new Vector3(
                 0,
                 speed * Time.deltaTime * 1.05f,
                 0);
        if (actions.graphics.gfxTransform.localPosition.y < 0)
        {
            jumpAudio.Play();
            goUp = true;
        }
    }

    public void DetectEnemy(Collider2D collider)
    {
        if (actions.target == null)
        {
            actions.target = collider.transform;
            ChangeState("Alert");
        }
    }

    public void AttackEnemy(Collider2D collider)
    {
        actions.target = collider.transform;
        var children = GetComponentsInChildren<EnemyDetector>();
        for (int i = 0; i < children.Length; i++)
        {
            Destroy(children[i].gameObject);
        }
        StartCoroutine(TriggerAttackIntervals());
    }

    public IEnumerator TriggerAttackIntervals()
    {
        while (!isDead)
        {
            ChangeState("Attack");
            yield return new WaitForSeconds(2.5f);
        }
    }
}