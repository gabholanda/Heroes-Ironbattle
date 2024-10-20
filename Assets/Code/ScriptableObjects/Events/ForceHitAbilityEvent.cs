using UnityEngine;

[CreateAssetMenu(fileName = "ForceHitAbilityEvent", menuName = "ScriptableObjects/Events/New Force Hit Event")]
public class ForceHitAbilityEvent : OnAbilityHitEvent
{
    public float force;

    public override void Raise(GameObject caster, Collider2D collision)
    {
        Rigidbody2D playerRb = collision.GetComponent<Rigidbody2D>();
        StateMachine sm = collision.GetComponent<StateMachine>();
        if (!sm.isDead)
        {
            CharacterMovement movement = collision.GetComponent<CharacterMovement>();
            if (movement)
                movement.SetVector(new Vector2(0, 0));
            playerRb.velocity *= 0;
            playerRb.AddForce(GetRandomForce() * force, ForceMode2D.Impulse);
        }
    }

    private Vector3 GetRandomForce()
    {
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);
        return new Vector3(x, y, 0).normalized;
    }
}
