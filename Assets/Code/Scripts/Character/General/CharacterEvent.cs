using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterEvent : MonoBehaviour
{
    public Dictionary<string, GameEvent> events;

    public CharacterEvent InitializeEvents(List<GameEvent> list)
    {
        events = new Dictionary<string, GameEvent>();

        list.ForEach(gameEvent =>
        {
            events.Add(gameEvent.name, gameEvent);
        });

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
