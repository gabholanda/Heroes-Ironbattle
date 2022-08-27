using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Element", menuName = "ScriptableObjects/Element/New Element")]
public class Element : ScriptableObject
{
    public Sprite icon;
    public string resistanceIcon;
    public string affinityIcon;
    [Range(-9.99f, 9.99f)]
    public float resistance;
    [Range(-9.99f, 9.99f)]
    public float affinity;

    [Header("UI")]
    public Color vertexColor;
    [ColorUsage(true, true)]
    public Color outlineColor;
    [ColorUsage(true, true)]
    public Color glowColor;
}
