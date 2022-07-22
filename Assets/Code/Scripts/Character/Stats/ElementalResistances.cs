using System;
using UnityEngine;

[Serializable]
public class ElementalResistances
{

    public ElementalResistances() { }
    public ElementalResistances(float fire, float ice, float dark, float lightning)
    {
        Fire = fire;
        Ice = ice;
        Dark = dark;
        Lightning = lightning;
    }

    [SerializeField]
    [Range(0, 100)]
    private float _fire;
    public float Fire { get { return _fire; } set { _fire = value; } }

    [SerializeField]
    [Range(0, 100)]
    private float _ice;
    public float Ice { get { return _ice; } set { _ice = value; } }

    [SerializeField]
    [Range(0, 100)]
    private float _dark;
    public float Dark { get { return _dark; } set { _dark = value; } }

    [SerializeField]
    [Range(0, 100)]
    private float _lightning;
    public float Lightning { get { return _lightning; } set { _lightning = value; } }

    public static ElementalResistances operator +(ElementalResistances a, ElementalResistances b)
      => new ElementalResistances(a.Fire + b.Fire, a.Ice + b.Ice, a.Dark + b.Dark, a.Lightning + b.Lightning);

    public static ElementalResistances operator -(ElementalResistances a, ElementalResistances b)
      => new ElementalResistances(a.Fire - b.Fire, a.Ice - b.Ice, a.Dark - b.Dark, a.Lightning - b.Lightning);

    public override string ToString()
    {
        return "Fire: " + Fire + "\n" +
            "Ice: " + Ice + "\n" +
            "Dark: " + Dark + "\n" +
            "Lightning: " + Lightning + "\n";
    }
}
