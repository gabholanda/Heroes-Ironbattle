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
    public Dictionary<string, Element> elements = new Dictionary<string, Element>();
    public List<Element> elementsToAdd;
    public List<ResistanceAdjuster> resistances;

    public void SetCharacterStats(CharacterBaseStats baseStats)
    {
        SetCombatStats(baseStats);
        SetResourcesStats(baseStats);
        SetDefensesResistances(baseStats);
        SetElements(baseStats);
        SetResistanceAdjuster(baseStats);
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

    public void SetElements(CharacterBaseStats baseStats)
    {

        baseStats.stats.elementsToAdd.ForEach(element =>
        {
            Element newElement = ScriptableObject.Instantiate(element);
            newElement.name = element.name;
            elements.Add(element.name, newElement);
        });
    }

    public void SetResistanceAdjuster(CharacterBaseStats baseStats)
    {
        baseStats.stats.resistances.ForEach(resistance =>
        {
            elements[resistance.element.name].resistance += resistance.value;
        });
    }

    internal void Deconstruct(
        out ResourcesStats resources,
        out DefensiveStats defenses,
        out Dictionary<string, Element> elementalResistances)
    {
        resources = this.resources;
        defenses = this.defensesResistances;
        elementalResistances = this.elements;
    }

    public override string ToString()
    {

        return resources.ToString() + "\n" +
            combatStats.ToString() + "\n" +
            defensesResistances.ToString() + "\n" +
            elements.ToString() + "\n";
    }
}
