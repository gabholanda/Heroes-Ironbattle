using UnityEngine;

public class ResourcesStats
{
    public ResourcesStats(
        float currentHealth,
        float maxHealth,
        float barrier,
        float maxEnergy,
        float regenRate)
    {
        CurrentHealth = currentHealth;
        MaxHealth = maxHealth;
        Barrier = barrier;
        CurrentEnergy = maxEnergy;
        MaxEnergy = maxEnergy;
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
    public float CurrentEnergy { get; set; }
    public float MaxEnergy { get; set; }
    public float RegenRate { get; set; }

}
