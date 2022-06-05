using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "ScriptableObjects/Character/Stats")]
public class CharacterBaseStats: ScriptableObject
{
    [Header("Flat Stats")]
    [Min(1)]
    public int strength;
    [Min(1)]
    public int intelligence;
    [Min(1)]
    public int dexterity;
    [Min(1)]
    public int vitality;
    [Min(0)]
    public int baseDefense;
    [Min(0)]
    public int baseMagicResistance;

    [Header("Elemental Resistances(Percentage based)")]

    [Range(0.0f, 99.9f)]
    public float fire;
    [Range(0.0f, 99.9f)]
    public float ice;
    [Range(0.0f, 99.9f)]
    public float dark;
    [Range(0.0f, 99.9f)]
    public float lightning;

    [Header("Resource Stats")]

    [Min(0f)]
    public float maxHealth;
    [Min(0f)]
    public float barrier;
    [Min(0f)]
    [Tooltip("Each Class will have a different Energy Name")]
    public float maxEnergy;
    [Min(0.1f)]
    public float regenRate;
    [Min(0f)]
    public float moveSpeed;

}
