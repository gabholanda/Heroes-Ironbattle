using UnityEngine;
using ScriptableObjectDropdown;

[CreateAssetMenu(fileName = "Resistance Adjuster", menuName = "ScriptableObjects/New Resistance Adjuster")]
public class ResistanceAdjuster : ScriptableObject
{
    [ScriptableObjectDropdown(grouping = ScriptableObjectGrouping.ByFolderFlat)] public Element element;
    [Min(0f)]
    public float value;


    public void IncreaseAffinity(GameObject target)
    {
        CharacterStats stats = target.GetComponent<StateMachine>().stats;
        stats.elements[element.name].resistance += value;
    }

    public void DecreaseAffinity(GameObject target)
    {
        CharacterStats stats = target.GetComponent<StateMachine>().stats;
        stats.elements[element.name].resistance -= value;
    }

    public void MultiplyAffinity(GameObject target)
    {
        CharacterStats stats = target.GetComponent<StateMachine>().stats;
        stats.elements[element.name].resistance *= value;
    }

    public void DivideAffinity(GameObject target)
    {
        CharacterStats stats = target.GetComponent<StateMachine>().stats;
        stats.elements[element.name].resistance /= value;
    }
}
