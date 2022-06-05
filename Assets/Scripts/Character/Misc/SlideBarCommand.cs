using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlideBarCommand : Command
{
    float intervalBetween;
    float normalizedValue;
    private Image image;
    Queue<SlideBarCommand> queue;
    float oldNormalizedValue;



    public SlideBarCommand(float _normalizedValue, Image _image, float _oldNormalizedValue, Queue<SlideBarCommand> _queue)
    {
        normalizedValue = _normalizedValue;
        image = _image;
        queue = _queue;
        oldNormalizedValue = _oldNormalizedValue;
    }
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

    private void IncrementImageFill(float value)
    {
        image.fillAmount += value;
    }
}
