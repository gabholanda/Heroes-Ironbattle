using System.Collections;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    public AbilityHandler handler;
    public Rigidbody2D rb;
    public GameObject caster;
    //protected void OnDestroy()
    //{

    //}

    protected IEnumerator StartSelfDestroyTimer()
    {
        float currentTime = 0f;
        if (handler.GetAbilityData().abilityDestroyDuration == 0)
            yield return null;

        while (handler.GetAbilityData().abilityDestroyDuration > currentTime)
        {
            yield return new WaitForSeconds(0.1f);
            currentTime += 0.1f;
        }
        Destroy(gameObject);
    }

    public void StartTimers()
    {
        StartCoroutine(this.StartSelfDestroyTimer());
    }

}
