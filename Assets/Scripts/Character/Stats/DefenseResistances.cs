using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseResistances
{
    public DefenseResistances(int defense, int magicResistance)
    {
        Defense = defense;
        MagicResistance = magicResistance;
    }
    public int Defense { get; set; }
    public int MagicResistance { get; set; }
}
