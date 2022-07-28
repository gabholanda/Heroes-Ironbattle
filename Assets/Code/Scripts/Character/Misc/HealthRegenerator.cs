using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthRegenerator : MonoBehaviour
{
    private ResourcesStats resources;
    private SlidingBar healthBar;

    public HealthRegenerator SetResources(ResourcesStats _resources)
    {
        resources = _resources;
        return this;
    }

    public HealthRegenerator SetManaBar(SlidingBar _healthBar)
    {
        healthBar = _healthBar;
        return this;
    }

    public void StartRegeneration()
    {
        StartCoroutine(RegenerateMana());
    }

    private IEnumerator RegenerateMana()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            float current = resources.CurrentHealth;
            if (resources.CurrentHealth < resources.MaxHealth)
            {
                resources.CurrentHealth += resources.HealthRegen;
            }
            else if (resources.CurrentHealth > resources.MaxHealth)
            {
                resources.CurrentHealth = resources.MaxHealth;
            }
            if (current != resources.CurrentHealth)
                healthBar.UpdateBar(resources.CurrentHealth / resources.MaxHealth);
        }
    }
}
