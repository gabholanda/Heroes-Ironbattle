using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DamageMethods
{
    public static void StandardDamageDealing(CharacterStats stats, float finalDamage)
    {
        if (stats.Barrier > 0)
        {
            float remainingDamage = stats.Barrier - finalDamage;
            stats.Barrier -= finalDamage;
            finalDamage -= Mathf.Abs(remainingDamage);
        }
        stats.CurrentHealth -= finalDamage;
    }
}
