using System;
using UnityEngine;


[Serializable]
public class CharacterStats
{
    public CharacterStats() { }

    [SerializeField]
    public ResourcesStats resources;
    [SerializeField]
    public CombatStats combatStats;
    [SerializeField]
    public DefenseResistances defensesResistances;
    [SerializeField]
    public ElementalResistances elementalResistances;

    public void SetCharacterStats(CharacterBaseStats baseStats)
    {
        SetCombatStats(baseStats);
        SetResourcesStats(baseStats);
        SetDefensesResistances(baseStats);
        SetElementalResistances(baseStats);
    }

    public void SetCombatStats(CharacterBaseStats baseStats)
    {
        combatStats = new CombatStats();
        combatStats += baseStats.stats.combatStats;
    }

    public void SetResourcesStats(CharacterBaseStats baseStats)
    {
        resources = new ResourcesStats();
        resources += baseStats.stats.resources;
    }

    public void SetDefensesResistances(CharacterBaseStats baseStats)
    {
        defensesResistances = new DefenseResistances();
        defensesResistances += baseStats.stats.defensesResistances;
    }

    public void SetElementalResistances(CharacterBaseStats baseStats)
    {
        elementalResistances = new ElementalResistances();
        elementalResistances += baseStats.stats.elementalResistances;
    }

    internal void Deconstruct(
        out ResourcesStats resources,
        out DefenseResistances defenses,
        out ElementalResistances elementalResistances)
    {
        resources = this.resources;
        defenses = this.defensesResistances;
        elementalResistances = this.elementalResistances;
    }

    public override string ToString()
    {

        return resources.ToString() + "\n" +
            combatStats.ToString() + "\n" +
            defensesResistances.ToString() + "\n" +
            elementalResistances.ToString() + "\n";
    }
}
