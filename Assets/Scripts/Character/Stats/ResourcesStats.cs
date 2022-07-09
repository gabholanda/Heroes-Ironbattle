using System;
using UnityEngine;

[Serializable]
public class ResourcesStats
{
    public ResourcesStats() { }
    public ResourcesStats(
        float currentHealth,
        float maxHealth,
        float maxMana,
        float regenRate)
    {
        MaxHealth = maxHealth;
        CurrentHealth = currentHealth;
        MaxMana = maxMana;
        CurrentMana = maxMana;
        RegenRate = regenRate;
    }

    [SerializeField]
    [Min(0)]
    private float _currentHealth;
    public float CurrentHealth { get { return _currentHealth; } set { _currentHealth = value > MaxHealth ? MaxHealth : value; } }

    [SerializeField]
    [Min(0)]
    private float _maxHealth;
    public float MaxHealth { get { return _maxHealth; } set { _maxHealth = value; } }

    [SerializeField]
    [Min(0)]
    private float _currentMana;
    public float CurrentMana { get { return _currentMana; } set { _currentMana = value > MaxMana ? MaxMana : value; } }

    [SerializeField]
    [Min(0)]
    private float _maxMana;
    public float MaxMana { get { return _maxMana; } set { _maxMana = value; } }

    [SerializeField]
    [Min(0)]
    private float _regenRate;
    public float RegenRate { get { return _regenRate; } set { _regenRate = value < 0 ? 1 : value; } }

    public static ResourcesStats operator +(ResourcesStats a, ResourcesStats b)
    => new ResourcesStats(
        a.CurrentHealth + b.CurrentHealth,
        a.MaxHealth + b.MaxHealth,
        a.MaxMana + b.MaxMana,
        a.RegenRate + b.RegenRate);

    public static ResourcesStats operator -(ResourcesStats a, ResourcesStats b)
    => new ResourcesStats(
        a.CurrentHealth - b.CurrentHealth,
        a.MaxHealth - b.MaxHealth,
        a.MaxMana - b.MaxMana,
        a.RegenRate - b.RegenRate);

    public override string ToString()
    {
        return "CurrentHealth: " + CurrentHealth + "\n" +
            "MaxHealth: " + MaxHealth + "\n" +
            "CurrentMana: " + CurrentMana + "\n" +
            "MaxMana: " + MaxMana + "\n" +
            "RegenRate: " + RegenRate + "\n";
    }

}
