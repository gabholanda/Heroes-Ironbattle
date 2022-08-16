using UnityEngine;
using ScriptableObjectDropdown;

[CreateAssetMenu(fileName = "Affinity Adjuster", menuName = "ScriptableObjects/New Affinity Adjuster")]
public class AffinityAdjuster : ScriptableObject
{
    [ScriptableObjectDropdown(grouping = ScriptableObjectGrouping.ByFolderFlat)] public Element element;
    [Min(0f)]
    public float value;


    public void IncreaseAffinity(GameObject target)
    {
        CharacterStats stats = target.GetComponent<StateMachine>().stats;
        stats.elements[element.name].affinity += value;
    }

    public void DecreaseAffinity(GameObject target)
    {
        CharacterStats stats = target.GetComponent<StateMachine>().stats;
        stats.elements[element.name].affinity -= value;
    }

    public void MultiplyAffinity(GameObject target)
    {
        CharacterStats stats = target.GetComponent<StateMachine>().stats;
        stats.elements[element.name].affinity *= value;
    }

    public void DivideAffinity(GameObject target)
    {
        CharacterStats stats = target.GetComponent<StateMachine>().stats;
        stats.elements[element.name].affinity /= value;
    }
}
