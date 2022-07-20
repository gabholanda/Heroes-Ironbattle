using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Ability Adder", menuName = "ScriptableObjects/New Ability Adder")]
public class AbilityAdder : ScriptableObject
{
    [Header("Both lists size must match and have values")]
    public List<AbilityHandler> abilities;
    public List<float> timers;

    public void AddToAuto(GameObject target)
    {
        AutoAbilitySpawner spawner = target.GetComponentInChildren<AutoAbilitySpawner>();
        for (int i = 0; i < abilities.Count; i++)
        {
            if (i > timers.Count) timers[i] = 1f;
            spawner.Add(abilities[i], timers[i]);
        }
    }
}
