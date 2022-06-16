using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPhysics : MonoBehaviour
{
    public Rigidbody2D rb;
    public Collider2D col;
    public float effectArea;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    public bool IsStale()
    {
        return Mathf.Abs(rb.velocity.x) <= 0.5 && Mathf.Abs(rb.velocity.y) <= 0.5;
    }

    public Vector2 SetNewStandardPath()
    {
        return rb.position + Random.insideUnitCircle * effectArea;
    }

}
