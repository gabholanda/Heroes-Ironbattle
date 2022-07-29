using System;
using UnityEngine;
[Serializable]
public class CombatStats : IStats<CombatStats>
{
    public CombatStats() { }
    public CombatStats(int attackPower, float moveSpeed)
    {
        AttackPower = attackPower;
        MoveSpeed = moveSpeed;
    }
    [SerializeField]
    [Min(0)]
    private int _attackPower;
    public int AttackPower { get { return _attackPower; } set { _attackPower = value; } }

    [SerializeField]
    [Min(0)]
    private float _moveSpeed;
    public float MoveSpeed { get { return _moveSpeed; } set { _moveSpeed = value; } }

    public void IncreaseStats(CombatStats b)
    {
        this.AttackPower += b.AttackPower;
        this.MoveSpeed += b.MoveSpeed;
    }

    public void DecreaseStats(CombatStats b)
    {
        this.AttackPower -= b.AttackPower;
        this.MoveSpeed -= b.MoveSpeed;
    }

    public void SetStats(CombatStats b)
    {
        this.AttackPower = b.AttackPower;
        this.MoveSpeed = b.MoveSpeed;
    }

    public override string ToString()
    {
        return "AttackPower: " + AttackPower + "\n" +
            "MoveSpeed: " + MoveSpeed + "\n";
    }
}
