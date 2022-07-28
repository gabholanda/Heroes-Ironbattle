using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlidingBar : MonoBehaviour, UIResourceBar
{
    [SerializeField]
    private Image image;

    private float oldNormalizedValue = 1f;
    readonly Queue<SlideBarCommand> commandQueue = new Queue<SlideBarCommand>();

    public void UpdateBar(float normalizedValue)
    {
        commandQueue.Enqueue(new SlideBarCommand()
            .AddNormalizedValue(normalizedValue)
            .AddOldNormalizedValue(oldNormalizedValue)
            .AddImage(image)
            .AddQueue(commandQueue));
        oldNormalizedValue = normalizedValue;
    }

    private void Update()
    {
        if (commandQueue.Count > 0)
        {
            commandQueue.Peek().Execute(Time.deltaTime);
        }
    }
}
