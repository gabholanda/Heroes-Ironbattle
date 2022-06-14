using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeColor : MonoBehaviour
{
    private SpriteRenderer sprRenderer;
    [SerializeField]
    private float minimum;

    void Start()
    {
        sprRenderer = GetComponent<SpriteRenderer>();
        Color color = new Color();
        PickRandomColors(ref color);
        sprRenderer.color = color;
    }

    public void PickRandomColors(ref Color color)
    {
        color.a = 1f;
        color.r = Random.Range(minimum, 255f) / 255f;
        color.g = Random.Range(minimum, 255f) / 255f;
        color.b = Random.Range(minimum, 255f) / 255f;
    }
}
