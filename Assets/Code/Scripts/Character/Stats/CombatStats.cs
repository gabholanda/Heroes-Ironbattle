using System;
using UnityEngine;
[Serializable]
public class CombatStats : IStats<CombatStats>
{
    public CombatStats() { }
    public CombatStats(int strength, int intelligence, int dexterity, float moveSpeed)
    {
        Strength = strength;
        Intelligence = intelligence;
        Dexterity = dexterity;
        MoveSpeed = moveSpeed;
    }
    [SerializeField]
    [Min(0)]
    private int _strength;
    public int Strength { get { return _strength; } set { _strength = value; } }

    [SerializeField]
    [Min(0)]
    private int _intelligence;
    public int Intelligence { get { return _intelligence; } set { _intelligence = value; } }

    [SerializeField]
    [Min(0)]
    private int _dexterity;
    public int Dexterity { get { return _dexterity; } set { _dexterity = value; } }

    [SerializeField]
    [Min(0)]
    private float _moveSpeed;
    public float MoveSpeed { get { return _moveSpeed; } set { _moveSpeed = value; } }

    public void IncreaseStats(CombatStats b)
    {
        this.Strength += b.Strength;
        this.Intelligence += b.Intelligence;
        this.Dexterity += b.Dexterity;
        this.MoveSpeed += b.MoveSpeed;
    }

    public void DecreaseStats(CombatStats b)
    {
        this.Strength -= b.Strength;
        this.Intelligence -= b.Intelligence;
        this.Dexterity -= b.Dexterity;
        this.MoveSpeed -= b.MoveSpeed;
    }

    public override string ToString()
    {
        return "Stremgth: " + Strength + "\n" +
            "Ingelligence: " + Intelligence + "\n" +
            "Dexterity: " + Dexterity + "\n" +
            "MoveSpeed: " + MoveSpeed + "\n";
    }
}
