using UnityEngine;

[CreateAssetMenu(fileName = "Artifact", menuName = "ScriptableObjects/Artifacts/New Artifact")]
public class Artifact : ScriptableObject
{
    [Header("UI Info")]
    public Sprite icon;
    [TextArea(5, 20)]
    public string description;
    public Rarity rarity;
    public int quantity;
    public GameObject pickablePrefab;
    [Header("Events")]
    public ArtifactEvent onApplyEvent;
    public ArtifactEvent onUnapplyEvent;


    public CharacterStats stats;

    public void Apply(GameObject holder)
    {
        CharacterStats holderStats = holder.GetComponent<StateMachine>().stats;
        ApplyRawStats(holderStats);
        ApplyRawDefenses(holderStats);
        ApplyElementalResistances(holderStats);
        onApplyEvent?.Invoke(holder);
    }
    public void Unapply(GameObject holder)
    {
        CharacterStats holderStats = holder.GetComponent<StateMachine>().stats;
        UnpplyRawStats(holderStats);
        UnapplyRawDefenses(holderStats);
        UnapplyElementalResistances(holderStats);
        onUnapplyEvent?.Invoke(holder);
    }

    private void ApplyRawStats(CharacterStats holderStats)
    {
        holderStats.combatStats += stats.combatStats;
    }

    private void ApplyRawDefenses(CharacterStats holderStats)
    {
        holderStats.defensesResistances += stats.defensesResistances;
    }

    private void ApplyElementalResistances(CharacterStats holderStats)
    {
        holderStats.elementalResistances += stats.elementalResistances;
    }

    private void UnpplyRawStats(CharacterStats holderStats)
    {
        holderStats.combatStats -= stats.combatStats;
    }

    private void UnapplyRawDefenses(CharacterStats holderStats)
    {
        holderStats.defensesResistances -= stats.defensesResistances;
    }

    private void UnapplyElementalResistances(CharacterStats holderStats)
    {
        holderStats.elementalResistances -= stats.elementalResistances;
    }

}
