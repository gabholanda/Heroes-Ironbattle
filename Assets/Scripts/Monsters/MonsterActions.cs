using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterActions : MonoBehaviour
{
    [Header("Components")]
    public MonsterPhysics physics;
    public MonsterGraphics graphics;
    public MonsterSeekerAI seekerAI;
    public StateMachine sm;

    public Transform target;

    [Header("Abilities")]
    public List<AbilityHandler> abilities = new List<AbilityHandler>();
    public Dictionary<string, AbilityHandler> abilitiesDict = new Dictionary<string, AbilityHandler>();
    private void Awake()
    {
        sm = GetComponent<StateMachine>();
        seekerAI = GetComponent<MonsterSeekerAI>();
        graphics = GetComponent<MonsterGraphics>();
        physics = GetComponent<MonsterPhysics>();
        abilities.ForEach(ability => ability.Initialize(gameObject));
    }

    public void SetMovement(float speed)
    {
        if (seekerAI.nodes is null)
            return;
        if (seekerAI.currentWayPoint == seekerAI.nodes.vectorPath.Count)
        {
            seekerAI.ResetWayPoint();
        }
        if (seekerAI.nodes?.vectorPath != null)
        {
            Vector2 direction = ((Vector2)seekerAI.nodes.vectorPath[seekerAI.currentWayPoint] - physics.rb.position).normalized;
            Vector2 force = direction * speed;
            physics.rb.AddForce(force);
            Flip();
            float distance = Vector2.Distance(physics.rb.position, seekerAI.nodes.vectorPath[seekerAI.currentWayPoint]);
            if (distance < seekerAI.newWaypointDistance)
            {
                seekerAI.currentWayPoint++;
            }
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
        if (physics.rb.velocity.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public void CanRepath(ref float checkRepathing, float checkRepathingTimer)
    {

        if (physics.IsStale())
        {
            checkRepathing += Time.deltaTime;
            if (checkRepathing > checkRepathingTimer)
            {
                Vector2 standardPath = physics.SetNewStandardPath();
                checkRepathing = 0f;
                seekerAI.ResetWayPoint();
                seekerAI.StartNewPath(physics.rb, standardPath);
            }
        }
        else
        {
            checkRepathing = 0;
        }
    }

    public void SeekTarget()
    {
        if (seekerAI.seeker.IsDone())
            seekerAI.seeker.StartPath(physics.rb.position, target.position, seekerAI.OnPathComplete);
    }
}
