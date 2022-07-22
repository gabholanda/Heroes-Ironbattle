using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour, UIResourceBar
{
    [SerializeField]
    private Image image;

    private float oldNormalizedHealth = 1f;
    readonly Queue<SlideBarCommand> commandQueue = new Queue<SlideBarCommand>();

    public void UpdateBar(float normalizedValue)
    {
        commandQueue.Enqueue(new SlideBarCommand()
            .AddNormalizedValue(normalizedValue)
            .AddOldNormalizedValue(oldNormalizedHealth)
            .AddImage(image)
            .AddQueue(commandQueue));
        oldNormalizedHealth = normalizedValue;
    }

    private void Update()
    {
        if (commandQueue.Count > 0)
        {
            commandQueue.Peek().Execute(Time.deltaTime);
        }
    }
}
