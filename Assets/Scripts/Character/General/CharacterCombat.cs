using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCombat : MonoBehaviour
{
    private AbilityHandler selectedAbility;
    public void SelectAbility(AbilityHandler ability)
    {
        selectedAbility = ability;
    }
    public void CancelAbility()
    {
        selectedAbility = null;
    }
    public void Cast(GameObject gObj, Vector2 v2)
    {
        selectedAbility.Execute(gObj, v2);
    }

    public bool canCast()
    {
        return selectedAbility != null && !selectedAbility.isCoolingDown;
    }
}
