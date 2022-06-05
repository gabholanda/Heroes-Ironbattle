using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour, UIResourceBar
{
    [SerializeField]
    private Image image;
    private ResourcesStats resources;

    private float oldNormalizedHealth = 1f;
    readonly Queue<SlideBarCommand> slideCommands = new Queue<SlideBarCommand>();


    public void SetStats(ResourcesStats _resources)
    {
        resources = _resources;
    }

    public void UpdateBar()
    {
        float normalizedValue = resources.CurrentHealth / resources.MaxHealth;
        slideCommands.Enqueue(new SlideBarCommand(normalizedValue, image, oldNormalizedHealth, slideCommands));
        oldNormalizedHealth = normalizedValue;
    }

    private void Update()
    {
        if (slideCommands.Count > 0)
        {
            slideCommands.Peek().Execute(Time.deltaTime);
        }
    }
}
