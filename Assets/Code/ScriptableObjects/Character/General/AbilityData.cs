using UnityEngine;

[CreateAssetMenu(fileName = "New Ability", menuName = "ScriptableObjects/Ability/New Ability")]
public class AbilityData : ScriptableObject
{
    [Header("UI/UX Data")]
    public AudioClip onCastSound;
    public AudioClip onHitSound;
    public Sprite icon;
    public Color color;
    [TextArea]
    public string description;

    [Header("Battle Data")]
    public ElementType element;
    public DamageType type;
    public float manaCost;
    public float cooldownDuration;
    [Tooltip("Check it if ability must start cooling down only after it ends")]
    public bool cooldownOnAbilityEnd;
    [Tooltip("This can be a timer or distance")]
    public float abilityDestroyDuration;
    public float scalingCoeficient;
}
