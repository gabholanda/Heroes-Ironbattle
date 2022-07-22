using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slow : StatusEffect
{
    private ParticleSystem ps;
    private CombatStats combatStats;
    public override void Apply()
    {
        Vector3 currentPosition = transform.position;
        transform.position = new Vector3(currentPosition.x + xOffset, currentPosition.y + yOffset, currentPosition.z);
        ps = GetComponent<ParticleSystem>();
        combatStats = target.GetComponent<StateMachine>().stats.combatStats;
        StartCoroutine(Tick());
    }

    public override IEnumerator Tick()
    {
        ps.Play();
        float percentageToReduce = (1 - effectValue);
        combatStats.MoveSpeed *= percentageToReduce;
        yield return new WaitForSeconds(duration);
        ps.Stop();
        combatStats.MoveSpeed = combatStats.MoveSpeed / percentageToReduce;
        Destroy(gameObject);
    }
}
