using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCombat : MonoBehaviour
{
    private AbilityHandler selectedAbility;
    private CharacterStats stats;
    private SlidingBar manaBar;

    public CharacterCombat SetStats(CharacterStats _stats)
    {
        stats = _stats;
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

    public bool CanCast()
    {
        bool doesntHaveAnAbilitySelected = selectedAbility == null;
        if (doesntHaveAnAbilitySelected) return false;
        if (selectedAbility.isCoolingDown) return false;
        bool hasEnoughManaToCast = stats.CurrentMana > selectedAbility.GetAbilityData().manaCost;
        return hasEnoughManaToCast;
    }

    private void UpdateManaBar()
    {
        stats.CurrentMana -= selectedAbility.GetAbilityData().manaCost;
        float normalizedValue = stats.CurrentMana / stats.MaxMana;
        manaBar.UpdateBar(normalizedValue);
    }
}
