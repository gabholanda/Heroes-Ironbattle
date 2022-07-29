using UnityEngine;

public static class DamageFormulas
{
    public static float StandardFormula(Ability ability, GameObject caster)
    {
        CharacterStats stats = caster.GetComponent<StateMachine>().stats;
        float characterElementalScalingAffinity = stats.elementalAffinities[ability.handler.GetAbilityData().element];
        float scalingCoeficient = ability.handler.GetAbilityData().scalingCoeficient;
        return Mathf.Round(stats.combatStats.AttackPower * scalingCoeficient * characterElementalScalingAffinity);
    }
}
