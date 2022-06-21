using UnityEngine;

public class ResourcesStats
{
    public ResourcesStats(
        float currentHealth,
        float maxHealth,
        float barrier,
        float maxMana,
        float regenRate)
    {
        CurrentHealth = currentHealth;
        MaxHealth = maxHealth;
        Barrier = barrier;
        CurrentMana = maxMana;
        MaxMana = maxMana;
        RegenRate = regenRate;
    }

    public float CurrentHealth { get; set; }
    public float MaxHealth { get; set; }

    private float barrier;
    public float Barrier
    {
        get { return barrier; }
        set
        {
            if (value > barrier)
            {
                barrier = 0;
            }
            else
            {
                barrier -= value;
            }
        }
    }
    public float CurrentMana { get; set; }
    public float MaxMana { get; set; }
    public float RegenRate { get; set; }

}
