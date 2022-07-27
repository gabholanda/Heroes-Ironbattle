using System;
using UnityEngine;

[Serializable]
public class ResourcesStats : IStats<ResourcesStats>
{
    public ResourcesStats() { }
    public ResourcesStats(
        float currentHealth,
        float maxHealth,
        float maxMana,
        float manaRegen,
        float healthRegen)
    {
        MaxHealth = maxHealth;
        CurrentHealth = currentHealth;
        MaxMana = maxMana;
        CurrentMana = maxMana;
        ManaRegen = manaRegen;
        HealthRegen = healthRegen;
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

    [Min(0)]
    [SerializeField]
    private float _manaRegen;
    public float ManaRegen { get { return _manaRegen; } set { _manaRegen = value < 0 ? 1 : value; } }

    [Min(0)]
    [SerializeField]
    private float _healthRegen;
    public float HealthRegen { get { return _healthRegen; } set { _healthRegen = value < 0 ? 0 : value; } }

    public void IncreaseStats(ResourcesStats other)
    {
        this.MaxHealth += other.MaxHealth;
        this.CurrentHealth += other.CurrentHealth;
        this.MaxMana += other.MaxMana;
        this.CurrentMana += other.CurrentMana;
        this.ManaRegen += other.ManaRegen;
        this.HealthRegen += other.HealthRegen;
    }

    public void DecreaseStats(ResourcesStats other)
    {
        this.MaxHealth -= other.MaxHealth;
        this.CurrentHealth -= other.CurrentHealth;
        this.MaxMana -= other.MaxMana;
        this.ManaRegen -= other.ManaRegen;
        this.HealthRegen -= other.HealthRegen;
    }

    public override string ToString()
    {
        return "CurrentHealth: " + CurrentHealth + "\n" +
            "MaxHealth: " + MaxHealth + "\n" +
            "HealthRegen: " + HealthRegen + "\n" +
            "CurrentMana: " + CurrentMana + "\n" +
            "MaxMana: " + MaxMana + "\n" +
            "ManaRegen: " + ManaRegen + "\n";
    }

}
