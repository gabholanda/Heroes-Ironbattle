using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DamageMethods
{
    public static void StandardDamageDealing(ResourcesStats resources, float finalDamage)
    {
        resources.CurrentHealth -= finalDamage;
    }
}
