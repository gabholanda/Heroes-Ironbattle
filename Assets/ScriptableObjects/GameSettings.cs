using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Settings", menuName = "ScriptableObjects/Settings/New Settings")]
public class GameSettings : ScriptableObject
{
    public float musicVolume;
    public float effectsVolume;
    public float masterVolume;
    public String resolution;
    public bool fullscreen;
}
