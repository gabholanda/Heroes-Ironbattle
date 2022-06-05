using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DamageMethods
{
    public static void StandardDamageDealing(ResourcesStats resources, float finalDamage)
    {
        if (resources.Barrier > 0)
        {
            float remainingDamage = resources.Barrier - finalDamage;
            resources.Barrier -= finalDamage;
            finalDamage -= Mathf.Abs(remainingDamage);
        }
        resources.CurrentHealth -= finalDamage;
    }
}
