public class CharacterStats
{
    public CharacterStats() { }

    public ResourcesStats resources;
    public CombatStats combatStats;
    public DefenseResistances defensesResistances;
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
        combatStats = new CombatStats(
            baseStats.strength,
            baseStats.intelligence,
            baseStats.dexterity,
            baseStats.vitality,
            baseStats.moveSpeed
            );
    }

    public void SetResourcesStats(CharacterBaseStats baseStats)
    {
        resources = new ResourcesStats(
            baseStats.maxHealth,
            baseStats.maxHealth,
            baseStats.barrier,
            baseStats.maxMana,
            baseStats.regenRate
            );
    }

    public void SetDefensesResistances(CharacterBaseStats baseStats)
    {
        defensesResistances = new DefenseResistances(
            baseStats.baseDefense,
            baseStats.baseMagicResistance
            );
    }

    public void SetElementalResistances(CharacterBaseStats baseStats)
    {
        elementalResistances = new ElementalResistances(
            baseStats.fire,
            baseStats.ice,
            baseStats.dark,
            baseStats.lightning
            );
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
}
