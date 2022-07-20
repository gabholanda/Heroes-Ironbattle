using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAbilitySpawner : MonoBehaviour
{
    private IEnumerator Spawn(KeyValuePair<AbilityHandler, float> timedAbility, Vector2 position)
    {
        while (this != null)
        {
            yield return new WaitForSeconds(timedAbility.Value);
            timedAbility.Key.Initialize(transform.parent.gameObject);
            timedAbility.Key.Execute(transform.parent.gameObject, position);
        }
    }

    public void Add(AbilityHandler ability, float time)
    {
        var kv = new KeyValuePair<AbilityHandler, float>(ability, time);
        StartCoroutine(Spawn(kv, transform.localPosition));
    }

    public void Add(AbilityHandler ability, float time, Vector2 position)
    {
        var kv = new KeyValuePair<AbilityHandler, float>(ability, time);
        StartCoroutine(Spawn(kv, position));
    }
}
