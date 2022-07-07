using System;
using UnityEngine;

[Serializable]
public class DefenseResistances
{
    public DefenseResistances() { }
    public DefenseResistances(int defense, int magicResistance)
    {
        Defense = defense;
        MagicResistance = magicResistance;
    }

    [SerializeField]
    private int _defense;
    public int Defense { get { return _defense; } set { _defense = value; } }

    [SerializeField]
    private int _magicResistance;
    public int MagicResistance { get { return _magicResistance; } set { _magicResistance = value; } }

    public static DefenseResistances operator +(DefenseResistances a, DefenseResistances b)
       => new DefenseResistances(a.Defense + b.Defense, a.MagicResistance + b.MagicResistance);

    public static DefenseResistances operator -(DefenseResistances a, DefenseResistances b)
      => new DefenseResistances(a.Defense - b.Defense, a.MagicResistance - b.MagicResistance);

    public override string ToString()
    {
        return "Defense: " + Defense + "\n" +
            "MagicResistance: " + MagicResistance + "\n";
    }
}
