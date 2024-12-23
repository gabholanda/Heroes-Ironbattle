﻿using System.Collections;
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
        StartCoroutine(Regenerate());
    }

    private IEnumerator Regenerate()
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
            if (healthBar && current != resources.CurrentHealth)
                healthBar.UpdateBar(resources.CurrentHealth / resources.MaxHealth);
        }
    }
}
