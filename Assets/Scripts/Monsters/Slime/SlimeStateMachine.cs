using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class SlimeStateMachine : StateMachine
{
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

    public Rigidbody2D rb;
    public Collider2D col;
    public Animator anim;
    [HideInInspector]
    public Seeker seeker;
    [HideInInspector]
    public Path nodes;
    public int currentWayPoint = 0;
    public float newWaypointDistance;
    public Transform gfxTransform;
    public Material material;
    public AbilityHandler ability;

    public Transform target;

    // Jumping mechanics data
    public float goUpTime = 0.5f;
    public bool goUp = true;
    public float goDownTime = 0.3f;

    private void Awake()
    {
        seeker = GetComponent<Seeker>();
        material = GetComponentInChildren<SpriteRenderer>().material;
        stats = new CharacterStats();
        stats.SetCharacterStats(baseStats);
        ability.Initialize(gameObject, transform.position);
        CreateStateDictionary();
        InstantiateDefaultStates();
        AddDefaultStates();
        currentState = GetInitialState();
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

    public void UpdatePath()
    {
        if (currentWayPoint >= nodes?.vectorPath.Count)
        {
            Vector2 standardPath = rb.position + Random.insideUnitCircle * 8f;
            ResetWayPoint();
            StartNewPath(standardPath);
        }
    }

    public void SeekTarget()
    {
        if (seeker.IsDone())
            seeker.StartPath(rb.position, target.position, OnPathComplete);
    }
    public void StartNewPath(Vector2 _target)
    {
        seeker.StartPath(rb.position, _target, OnPathComplete);
    }

    public void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            nodes = p;
            currentWayPoint = 0;
        }
    }

    public void SetRepeat(float time, float frequency, string method)
    {
        InvokeRepeating(method, time, frequency);
    }

    public void StopRepeat(string method)
    {
        CancelInvoke(method);
    }

    public void Flip()
    {
        if (rb.velocity.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public void SetMovement(float speed)
    {
        if (nodes is null)
            return;
        if (currentWayPoint == nodes.vectorPath.Count)
        {
            ResetWayPoint();
        }
        Vector2 direction = ((Vector2)nodes.vectorPath[currentWayPoint] - rb.position).normalized;
        Vector2 force = direction * speed;
        rb.AddForce(force);
        Flip();
        float distance = Vector2.Distance(rb.position, nodes.vectorPath[currentWayPoint]);
        if (distance < newWaypointDistance)
        {
            currentWayPoint++;
        }
    }

    public void GoUp(float speed)
    {
        gfxTransform.localPosition += new Vector3(
                0,
                speed * Time.deltaTime,
                0);
        if (gfxTransform.localPosition.y > 0.5f)
        {
            goUp = false;
        }
    }

    public void GoDown(float speed)
    {
        gfxTransform.localPosition -= new Vector3(
                 0,
                 speed * Time.deltaTime * 1.05f,
                 0);
        if (gfxTransform.localPosition.y < 0)
        {
            goUp = true;
        }
    }

    public void ResetWayPoint()
    {
        currentWayPoint = 0;
    }

    public void DetectEnemy(Collider2D collider)
    {
        if (target == null)
        {
            target = collider.transform;
            this.ChangeState(this.alertState);
        }
    }

    public void AttackEnemy(Collider2D collider)
    {
        target = collider.transform;
        var children = GetComponentsInChildren<EnemyDetector>();
        for (int i = 0; i < children.Length; i++)
        {
            Destroy(children[i].gameObject);
        }
        StartCoroutine(TriggerAttackIntervals());
    }

    public IEnumerator TriggerAttackIntervals()
    {
        while (true)
        {
            this.ChangeState(this.attackState);
            yield return new WaitForSeconds(2.5f);
        }
    }
}