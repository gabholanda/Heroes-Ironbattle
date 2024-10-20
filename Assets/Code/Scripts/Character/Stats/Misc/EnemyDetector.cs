using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    public List<string> tagsToDetect = new List<string>();
    public CircleCollider2D circle;
    public string ExecutionMethod;
    public Color gizmosColor;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (tagsToDetect.Find(tag => tag == collision.tag) != null)
        {
            SendMessageUpwards(ExecutionMethod, collision);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmosColor;
        Gizmos.DrawWireSphere(transform.position, circle.radius);
    }
}
