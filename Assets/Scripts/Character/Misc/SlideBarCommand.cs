using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlideBarCommand : Command
{
    float normalizedValue;
    private Image image;
    Queue<SlideBarCommand> queue;
    float oldNormalizedValue;
    public override void Execute() { }

    public void Execute(float value)
    {
        oldNormalizedValue -= value / 5;
        if (oldNormalizedValue <= normalizedValue)
        {
            SetImageFill(normalizedValue);
            queue.Dequeue();
            return;
        }
        SetImageFill(oldNormalizedValue);
    }

    private void SetImageFill(float value)
    {
        image.fillAmount = value;
    }

    public SlideBarCommand AddNormalizedValue(float _normalizedvalue)
    {
        normalizedValue = _normalizedvalue;
        return this;
    }

    public SlideBarCommand AddImage(Image _image)
    {
        image = _image;
        return this;
    }
    public SlideBarCommand AddOldNormalizedValue(float _oldNormalizedValue)
    {
        oldNormalizedValue = _oldNormalizedValue;
        return this;
    }
    public SlideBarCommand AddQueue(Queue<SlideBarCommand> _queue)
    {
        queue = _queue;
        return this;
    }
}
