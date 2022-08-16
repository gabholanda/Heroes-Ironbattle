using UnityEngine;

public static class DamageFormulas
{
    public static float StandardFormula(Ability ability, GameObject caster)
    {
        CharacterStats stats = caster.GetComponent<StateMachine>().stats;
        float characterElementalScalingAffinity = stats.elements[ability.handler.GetAbilityData().element.name].affinity;
        float scalingCoeficient = ability.handler.GetAbilityData().scalingCoeficient;
        return Mathf.Round(stats.combatStats.AttackPower * scalingCoeficient * characterElementalScalingAffinity);
    }
}
