using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burn : StatusEffect
{
    private DamageReceiver receiver;
    private ParticleSystem ps;
    public override void Apply()
    {
        Vector3 currentPosition = transform.position;
        transform.position = new Vector3(currentPosition.x + xOffset, currentPosition.y + yOffset, currentPosition.z);
        dealer = DamageMethods.StandardDamageDealing;
        receiver = target.GetComponent<DamageReceiver>();
        ps = GetComponent<ParticleSystem>();
        StartCoroutine(Tick());
    }

    public override IEnumerator Tick()
    {
        ps.Play();
        while (duration > current)
        {
            receiver.ReceiveDamage(effectValue, type, element, dealer);
            yield return new WaitForSeconds(timeCheck);
            current += timeCheck;
        }
        ps.Stop();
        Destroy(gameObject);
    }
}
