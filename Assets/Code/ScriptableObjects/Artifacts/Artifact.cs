using UnityEngine;

[CreateAssetMenu(fileName = "Artifact", menuName = "ScriptableObjects/Artifacts/New Artifact")]
public class Artifact : ScriptableObject
{
    [Header("UI Info")]
    public Sprite icon;
    [TextArea(5, 20)]
    public string description;
    public Rarity rarity;
    [Header("Events")]
    public ArtifactEvent onApplyEvent;
    public ArtifactEvent onUnapplyEvent;


    public CharacterStats stats;

    public void Apply(GameObject holder)
    {
        CharacterStats holderStats = holder.GetComponent<StateMachine>().stats;
        applyCombatStats(holderStats);
        applyResourcesStats(holderStats);
        ApplyRawDefenses(holderStats);
        ApplyElementalResistances(holderStats);
        onApplyEvent?.Invoke(holder);
    }
    public void Unapply(GameObject holder)
    {
        CharacterStats holderStats = holder.GetComponent<StateMachine>().stats;
        UnapplyCombatStats(holderStats);
        UnapplyResourcesStats(holderStats);
        UnapplyRawDefenses(holderStats);
        UnapplyElementalResistances(holderStats);
        onUnapplyEvent?.Invoke(holder);
    }

    private void applyCombatStats(CharacterStats holderStats)
    {
        holderStats.combatStats.IncreaseStats(stats.combatStats);
    }

    private void applyResourcesStats(CharacterStats holderStats)
    {
        holderStats.resources.IncreaseStats(stats.resources);
    }

    private void ApplyRawDefenses(CharacterStats holderStats)
    {
        holderStats.defensesResistances.IncreaseStats(stats.defensesResistances);
    }

    private void ApplyElementalResistances(CharacterStats holderStats)
    {
        holderStats.elementalResistances.IncreaseStats(stats.elementalResistances);
    }

    private void UnapplyCombatStats(CharacterStats holderStats)
    {
        holderStats.combatStats.DecreaseStats(stats.combatStats);
    }

    private void UnapplyResourcesStats(CharacterStats holderStats)
    {
        holderStats.resources.DecreaseStats(stats.resources);
    }

    private void UnapplyRawDefenses(CharacterStats holderStats)
    {
        holderStats.defensesResistances.DecreaseStats(stats.defensesResistances);
    }

    private void UnapplyElementalResistances(CharacterStats holderStats)
    {
        holderStats.elementalResistances.DecreaseStats(stats.elementalResistances);
    }

}
