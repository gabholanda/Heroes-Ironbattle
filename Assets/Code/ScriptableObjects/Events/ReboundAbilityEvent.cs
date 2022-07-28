using UnityEngine;

[CreateAssetMenu(fileName = "Rebound Ability Event", menuName = "ScriptableObjects/Events/New Rebound Ability Event")]
public class ReboundAbilityEvent : OnAbilityHitEvent
{
    public float force;

    public override void Raise(GameObject caster, Collider2D collision)
    {
        Rigidbody2D playerRb = collision.GetComponent<Rigidbody2D>();
        CharacterMovement movement = collision.GetComponent<CharacterMovement>();
        StateMachine sm = collision.GetComponent<StateMachine>();
        if (!sm.isDead)
        {
            if (movement)
                movement.SetVector(new Vector2(0, 0));
            playerRb.velocity *= 0;
            Vector3 dir = (collision.transform.position - caster.transform.position).normalized;
            playerRb.AddForce(force * dir, ForceMode2D.Impulse);
        }
    }
}
