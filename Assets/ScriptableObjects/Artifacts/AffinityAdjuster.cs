using UnityEngine;

[CreateAssetMenu(fileName = "Affinity Adjuster", menuName = "ScriptableObjects/New Affinity Adjuster")]
public class AffinityAdjuster : ScriptableObject
{
    public ElementType element;
    [Min(0.1f)]
    public float value;


    public void IncreaseAffinity(GameObject target)
    {
        CharacterStats stats = target.GetComponent<StateMachine>().stats;
        stats.elementalAffinities[element] += value;
    }

    public void DecreaseAffinity(GameObject target)
    {
        CharacterStats stats = target.GetComponent<StateMachine>().stats;
        stats.elementalAffinities[element] -= value;
    }

    public void MultiplyAffinity(GameObject target)
    {
        CharacterStats stats = target.GetComponent<StateMachine>().stats;
        stats.elementalAffinities[element] *= value;
    }

    public void DivideAffinity(GameObject target)
    {
        CharacterStats stats = target.GetComponent<StateMachine>().stats;
        stats.elementalAffinities[element] /= value;
    }
}
