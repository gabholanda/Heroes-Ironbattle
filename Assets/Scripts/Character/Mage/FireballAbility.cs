using UnityEngine;

public class FireballAbility : Ability
{
    ProjectileHandler newHandler;
    private void OnEnable()
    {
        newHandler = (ProjectileHandler)handler;
    }
    private void Update()
    {
        rb.AddForce(newHandler.dir * newHandler.projectileSpeed);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //ps.Play();
        Destroy(gameObject);
    }

}
