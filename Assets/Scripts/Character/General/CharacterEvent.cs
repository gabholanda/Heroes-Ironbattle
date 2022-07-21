using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterEvent : MonoBehaviour
{
    public Dictionary<string, GameEvent> events;

    public CharacterEvent InitializeEvents()
    {
        events = new Dictionary<string, GameEvent>()
        {
            {"Dash", ScriptableObject.CreateInstance<GameEvent>() },
            {"Cast", ScriptableObject.CreateInstance<GameEvent>() },
            {"Hurt", ScriptableObject.CreateInstance<GameEvent>() }
        };
        return this;
    }
    public CharacterEvent AddListeners()
    {
        foreach (var kv in events)
        {
            GameEventListener listener = gameObject.AddComponent<GameEventListener>();
            listener.Event = kv.Value;
        }

        return this;
    }
}
