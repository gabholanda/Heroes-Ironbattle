using System;
using UnityEngine;

[Serializable]
public class DefensiveStats : IStats<DefensiveStats>
{
    public DefensiveStats() { }
    public DefensiveStats(int armor, int magicResistance)
    {
        Armor = armor;
        MagicResistance = magicResistance;
    }

    [SerializeField]
    private int _armor;
    public int Armor { get { return _armor; } set { _armor = value; } }

    [SerializeField]
    private int _magicResistance;
    public int MagicResistance { get { return _magicResistance; } set { _magicResistance = value; } }

    public void IncreaseStats(DefensiveStats b)
    {
        this.Armor += b.Armor;
        this.MagicResistance += b.MagicResistance;
    }

    public void DecreaseStats(DefensiveStats b)
    {
        this.Armor -= b.Armor;
        this.MagicResistance -= b.MagicResistance;
    }

    public void SetStats(DefensiveStats b)
    {
        this.Armor = b.Armor;
        this.MagicResistance = b.MagicResistance;
    }

    public override string ToString()
    {
        return "Armor: " + Armor + "\n" +
            "MagicResistance: " + MagicResistance + "\n";
    }
}
