﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaRegenerator : MonoBehaviour
{
    private ResourcesStats resources;
    private SlidingBar manaBar;

    public ManaRegenerator SetResources(ResourcesStats _resources)
    {
        resources = _resources;
        return this;
    }

    public ManaRegenerator SetManaBar(SlidingBar _manaBar)
    {
        manaBar = _manaBar;
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
            if (resources.CurrentMana < resources.MaxMana)
            {
                resources.CurrentMana += resources.ManaRegen;
            }
            else if (resources.CurrentMana > resources.MaxMana)
            {
                resources.CurrentMana = resources.MaxMana;
            }
            if (resources.MaxMana != resources.CurrentMana)
                manaBar.UpdateBar(resources.CurrentMana / resources.MaxMana);

        }
    }
}
