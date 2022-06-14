using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCombat : MonoBehaviour
{
    private AbilityHandler selectedAbility;
    private ResourcesStats resources;
    private SlidingBar manaBar;

    public CharacterCombat SetResources(ResourcesStats _resources)
    {
        resources = _resources;
        return this;
    }

    public CharacterCombat SetManaBar(SlidingBar _manaBar)
    {
        manaBar = _manaBar;
        return this;
    }

    public void SelectAbility(AbilityHandler ability)
    {
        selectedAbility = ability;
    }
    public void CancelAbilitySelection()
    {
        selectedAbility = null;
    }
    public void Cast(GameObject gObj, Vector2 v2)
    {
        UpdateManaBar();
        selectedAbility.Execute(gObj, v2);
    }

    public bool canCast()
    {
        bool doesntHaveAnAbilitySelected = selectedAbility == null;
        if (doesntHaveAnAbilitySelected) return false;
        if (selectedAbility.isCoolingDown) return false;
        bool hasEnoughManaToCast = resources.CurrentMana > selectedAbility.GetAbilityData().manaCost;
        return hasEnoughManaToCast;
    }

    private void UpdateManaBar()
    {
        resources.CurrentMana -= selectedAbility.GetAbilityData().manaCost;
        float normalizedValue = resources.CurrentMana / resources.MaxMana;
        manaBar.UpdateBar(normalizedValue);
    }
}
