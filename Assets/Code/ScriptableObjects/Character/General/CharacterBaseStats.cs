using UnityEngine;
using System.Linq;
using UnityEditor;

[CreateAssetMenu(fileName = "Stats", menuName = "ScriptableObjects/Character/Stats")]
public class CharacterBaseStats : ScriptableObject
{
    public CharacterStats stats;

    private void OnEnable()
    {
        stats.elementsToAdd = Resources.LoadAll<Element>("Elements").ToList();
    }
}
