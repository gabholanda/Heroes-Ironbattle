using System.Collections;
using System.Collections.Generic;
using ScriptableObjectDropdown;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    [ScriptableObjectDropdown(grouping = ScriptableObjectGrouping.ByFolderFlat)] public Element element;

    private void Start()
    {
        Element newElement = ScriptableObject.CreateInstance<Element>();
        newElement.name = element.name;
        newElement.icon = element.icon;
        newElement.affinityIcon = element.affinityIcon;
        newElement.resistanceIcon = element.resistanceIcon;
        newElement.affinity = element.affinity;
        newElement.resistance = element.resistance;
    }
}
