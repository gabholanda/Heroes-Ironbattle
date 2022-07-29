using System;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class CharacterStats
{
    public CharacterStats() { }

    [SerializeField]
    public ResourcesStats resources = new ResourcesStats();
    [SerializeField]
    public CombatStats combatStats = new CombatStats();
    [SerializeField]
    public DefensiveStats defensesResistances = new DefensiveStats();
    [SerializeField]
    public ElementalResistances elementalResistances = new ElementalResistances();
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
        combatStats.SetStats(baseStats.stats.combatStats);
    }

    public void SetResourcesStats(CharacterBaseStats baseStats)
    {
        resources.SetStats(baseStats.stats.resources);
    }

    public void SetDefensesResistances(CharacterBaseStats baseStats)
    {
        defensesResistances.SetStats(baseStats.stats.defensesResistances);
    }

    public void SetElementalResistances(CharacterBaseStats baseStats)
    {
        elementalResistances.SetStats(baseStats.stats.elementalResistances);
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
        out DefensiveStats defenses,
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
