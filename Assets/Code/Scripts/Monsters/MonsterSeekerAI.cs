using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class MonsterSeekerAI : MonoBehaviour
{

    [Header("AI Seeker")]
    [HideInInspector]
    public Seeker seeker;
    [HideInInspector]
    public Path nodes;
    public int currentWayPoint = 0;
    public float newWaypointDistance;
    // Start is called before the first frame update
    void Awake()
    {
        seeker = GetComponent<Seeker>();
    }

    public void ResetWayPoint()
    {
        currentWayPoint = 0;
        nodes = null;
    }


    public void UpdatePath(Rigidbody2D rb)
    {
        if (currentWayPoint >= nodes?.vectorPath.Count)
        {
            Vector2 standardPath = rb.position + Random.insideUnitCircle * 8f;
            ResetWayPoint();
            StartNewPath(rb, standardPath);
        }
    }

    public void StartNewPath(Rigidbody2D rb, Vector2 _target)
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
}
