using UnityEngine;

[CreateAssetMenu(fileName = "Artifact", menuName = "ScriptableObjects/Artifacts/New Artifact")]
public class Artifact : ScriptableObject
{
    [Header("UI Info")]
    public Sprite icon;
    [TextArea(5,20)]
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
        stats = holder.GetComponent<StateMachine>().stats;
        ApplyRawStats(stats);
        ApplyRawDefenses(stats);
        ApplyElementalResistances(stats);
        onApplyEvent?.Invoke(holder);
    }
    public void Unapply(GameObject holder)
    {
        CharacterStats stats = holder.GetComponent<StateMachine>().stats;
        UnpplyRawStats(stats);
        UnapplyRawDefenses(stats);
        UnapplyElementalResistances(stats);
        onUnapplyEvent?.Invoke(holder);
    }

    private void ApplyRawStats(CharacterStats stats)
    {
        stats.combatStats += stats.combatStats;
    }

    private void ApplyRawDefenses(CharacterStats stats)
    {
        stats.defensesResistances += stats.defensesResistances;
    }

    private void ApplyElementalResistances(CharacterStats stats)
    {
        stats.elementalResistances += stats.elementalResistances;
    }

    private void UnpplyRawStats(CharacterStats stats)
    {
        stats.combatStats -= stats.combatStats;
    }

    private void UnapplyRawDefenses(CharacterStats stats)
    {
        stats.defensesResistances -= stats.defensesResistances;
    }

    private void UnapplyElementalResistances(CharacterStats stats)
    {
        stats.elementalResistances -= stats.elementalResistances;
    }

}
