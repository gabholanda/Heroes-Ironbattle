using UnityEngine;


public delegate void TriggerExecuteCallback(GameObject g, Vector2 v2);
public class AbilityCallback
{
    private readonly GameObject caster;
    private Vector2 position;
    private readonly TriggerExecuteCallback callback;
    public AbilityCallback(GameObject g, Vector2 v2, TriggerExecuteCallback cb)
    {
        caster = g;
        position = v2;
        callback = cb;
    }

    public void TriggerCallback()
    {
        this.callback(caster, position);
    }
}
