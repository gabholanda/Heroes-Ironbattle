using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slow : StatusEffect
{
    private ParticleSystem ps;
    private CharacterStats stats;
    public override void Apply()
    {
        Vector3 currentPosition = transform.position;
        transform.position = new Vector3(currentPosition.x + xOffset, currentPosition.y + yOffset, currentPosition.z);
        ps = GetComponent<ParticleSystem>();
        stats = target.GetComponent<StateMachine>().stats;
        StartCoroutine(Tick());
    }

    public override IEnumerator Tick()
    {
        ps.Play();
        float percentageToReduce = (1 - effectValue);
        stats.MoveSpeed *= percentageToReduce;
        yield return new WaitForSeconds(duration);
        ps.Stop();
        stats.MoveSpeed = stats.MoveSpeed / percentageToReduce;
        Destroy(gameObject);
    }
}
