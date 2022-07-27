using System;
using System.Collections.Generic;
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
    [SerializeField]
    public Dictionary<ElementType, float> elementalAffinities = new Dictionary<ElementType, float>();


    public void SetCharacterStats(CharacterBaseStats baseStats)
    {
        SetCombatStats(baseStats);
        SetResourcesStats(baseStats);
        SetDefensesResistances(baseStats);
        SetElementalResistances(baseStats);
        AddAffinities();
    }

    public void SetCombatStats(CharacterBaseStats baseStats)
    {
        combatStats = new CombatStats();
        combatStats.IncreaseStats(baseStats.stats.combatStats);
    }

    public void SetResourcesStats(CharacterBaseStats baseStats)
    {
        resources = new ResourcesStats();
        resources.IncreaseStats(baseStats.stats.resources);
    }

    public void SetDefensesResistances(CharacterBaseStats baseStats)
    {
        defensesResistances = new DefenseResistances();
        defensesResistances.IncreaseStats(baseStats.stats.defensesResistances);
    }

    public void SetElementalResistances(CharacterBaseStats baseStats)
    {
        elementalResistances = new ElementalResistances();
        elementalResistances.IncreaseStats(baseStats.stats.elementalResistances);
    }

    public void AddAffinities()
    {
        elementalAffinities.Add(ElementType.Fire, 1);
        elementalAffinities.Add(ElementType.Ice, 1);
        elementalAffinities.Add(ElementType.Dark, 1);
        elementalAffinities.Add(ElementType.Lightning, 1);
        elementalAffinities.Add(ElementType.Neutral, 1);
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
