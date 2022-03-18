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

    public Rigidbody2D rb;
    public Animator anim;
    [HideInInspector]
    public Seeker seeker;
    [HideInInspector]
    public Path nodes;
    public int currentWayPoint = 0;
    //public bool reachedEndOfPath = false;
    public float newWaypointDistance;
    public Transform gfxTransform;

    public CharacterStats stats;

    public Transform target;

    // Jumping mechanics data
    public float goUpDur = 0f;
    public float goUpTime = 0.5f;
    public bool goUp = true;
    public float goDownDur = 0f;
    public float goDownTime = 0.3f;

    private void Awake()
    {
        idleState = new SlimeIdleState(this);
        patrollingState = new SlimePatrollingState(this);
        alertState = new SlimeAlertState(this);
        chasingState = new SlimeChasingState(this);
        attackState = new SlimeAttackState(this);
        seeker = GetComponent<Seeker>();
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
        goUpDur += Time.deltaTime;
        gfxTransform.localPosition += new Vector3(
                0,
                speed * Time.deltaTime,
                0);
        if (goUpDur > goUpTime)
        {
            goUpDur = 0f;
            goUp = false;
        }
    }

    public void GoDown(float speed)
    {
        goDownDur += Time.deltaTime;
        gfxTransform.localPosition -= new Vector3(
                 0,
                 speed * Time.deltaTime,
                 0);
        if (goDownDur > goDownTime)
        {
            goDownDur = 0f;
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
        this.ChangeState(this.attackState);
    }

    public void CheckAttack()
    {

    }
}