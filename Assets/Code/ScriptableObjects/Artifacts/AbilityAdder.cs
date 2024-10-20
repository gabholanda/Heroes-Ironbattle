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

    public void AddToOnHit(GameObject target)
    {
        List<AbilityHandler> targetHandlers = target.GetComponent<StateMachine>().handlers;
        targetHandlers.ForEach(handler =>
        {
            for (int j = 0; j < abilities.Count; j++)
            {
                handler.abilitiesToTriggerOnHit.Add(abilities[j]);
            }
        });
    }

    public void AddProjectileMultiCastToAuto(GameObject target)
    {
        AutoAbilitySpawner spawner = target.GetComponentInChildren<AutoAbilitySpawner>();
        for (int i = 0; i < abilities.Count; i++)
        {
            if (i > timers.Count) timers[i] = 1f;
            MultiCastHandler multicastHandler = (MultiCastHandler)abilities[i];

            multicastHandler.abilities.ForEach(handler =>
            {
                ProjectileHandler projectileHandler = (ProjectileHandler)handler;
                spawner.Add(projectileHandler, timers[i], projectileHandler.dir);
            });
        }
    }

    public void AddProjectileMultiCastToDash(GameObject caster)
    {
        GameEventListener actionListener = caster.GetComponentInChildren<GameEventListener>();
        if (actionListener != null)
        {
            actionListener.Event = caster.GetComponent<CharacterEvent>().events["Dash"];

            actionListener.TryRegister();
            for (int i = 0; i < abilities.Count; i++)
            {
                MultiCastHandler multicastHandler = (MultiCastHandler)abilities[i];
                multicastHandler.abilities.ForEach(handler =>
                {
                    ProjectileHandler projectileHandler = (ProjectileHandler)handler;
                    AbilityCallback abilityCallback = new AbilityCallback()
                    .AddCaster(caster)
                    .AddPosition(projectileHandler.dir)
                    .AddTriggerAbilityCallback(projectileHandler.Execute);
                    actionListener.Response.AddListener(abilityCallback.TriggerAbilityCallback);
                });
            }
        }
    }
}
