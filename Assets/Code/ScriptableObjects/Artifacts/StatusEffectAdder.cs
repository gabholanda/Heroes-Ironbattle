using ScriptableObjectDropdown;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Affinity Adjuster", menuName = "ScriptableObjects/New Status Effect Adder")]
public class StatusEffectAdder : ScriptableObject
{
    [ScriptableObjectDropdown(grouping = ScriptableObjectGrouping.ByFolderFlat)] public OnAbilityHitEvent onHitEffect;

    public void Add(GameObject target)
    {
        List<AbilityHandler> handlers = GetHandlers(target);
        handlers.ForEach(AddEffectToList);
    }

    public void Remove(GameObject target)
    {
        List<AbilityHandler> handlers = GetHandlers(target);
        handlers.ForEach(RemoveEffectFromList);
    }

    private List<AbilityHandler> GetHandlers(GameObject target)
    {
        return target.GetComponent<StateMachine>().handlers; ;
    }

    private void AddEffectToList(AbilityHandler handler)
    {
        handler.effectList.Add(onHitEffect);
    }

    private void RemoveEffectFromList(AbilityHandler handler)
    {
        handler.effectList.Remove(onHitEffect);
    }
}
