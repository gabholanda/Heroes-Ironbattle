using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "ScriptableObjects/Character/Stats")]
public class CharacterStats : ScriptableObject, ISerializationCallbackReceiver
{
    [Header("Flat Stats")]
    [Min(1)]
    public int strength;
    [Min(1)]
    public int intelligence;
    [Min(1)]
    public int dexterity;
    [Min(1)]
    public int vitality;
    [Min(0)]
    public int baseDefense;
    [Min(0)]
    public int baseMagicResistance;

    [Header("Elemental Resistances(Percentage based)")]

    [Range(0.0f, 99.9f)]
    public float fire;
    [Range(0.0f, 99.9f)]
    public float ice;
    [Range(0.0f, 99.9f)]
    public float dark;
    [Range(0.0f, 99.9f)]
    public float lightning;

    [Header("Resource Stats")]

    [Min(0f)]
    public float maxHealth;
    [Min(0f)]
    public float barrier;
    [Min(0f)]
    [Tooltip("Each Class will have a different Energy Name")]
    public float maxMana;
    [Min(0.1f)]
    public float regenRate;
    [Min(0f)]
    public float moveSpeed;

    public int Strength { get; set; }
    public int Intelligence { get; set; }
    public int Dexterity { get; set; }
    public int Vitality { get; set; }
    public int Defense { get; set; }
    public int MagicResistance { get; set; }
    public float Fire { get; set; }
    public float Ice { get; set; }
    public float Dark { get; set; }
    public float Lightning { get; set; }
    public float MaxHealth { get; set; }
    public float CurrentHealth { get; set; }

    [NonSerialized]
    private float _barrier;
    public float Barrier
    {
        get { return _barrier; }
        set
        {
            if (value > _barrier)
            {
                _barrier = 0;
            }
            else
            {
                _barrier -= value;
            }
        }
    }
    public float MaxMana { get; set; }
    public float CurrentMana { get; set; }
    public float RegenRate { get; set; }
    public float MoveSpeed { get; set; }

    public void OnAfterDeserialize()
    {
        Strength = strength;
        Intelligence = intelligence;
        Dexterity = dexterity;
        Vitality = vitality;

        Fire = fire;
        Ice = ice;
        Lightning = lightning;
        Dark = dark;

        MaxHealth = maxHealth;
        CurrentHealth = maxHealth;
        MaxMana = maxMana;
        CurrentMana = maxMana;
        Barrier = barrier;
        RegenRate = regenRate;
        MoveSpeed = moveSpeed;
    }

    public void OnBeforeSerialize()
    {

    }
}
