using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalResistances
{

    public ElementalResistances(float fire, float ice, float dark, float lightning)
    {
        Fire = fire;
        Ice = ice;
        Dark = dark;
        Lightning = lightning;
    }
    public float Fire { get; set; }
    public float Ice { get; set; }
    public float Dark { get; set; }
    public float Lightning { get; set; }
}
