
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine;

[CreateAssetMenu(fileName = "Input Reader", menuName = "ScriptableObjects/Input/Reader")]
public class InputReader : ScriptableObject
{
    public InputAction OnMove;
    public InputAction OnFire;
    public InputAction OnAbilitySelect;
    public InputAction OnAbilityCancel;
    public InputAction OnDash;
    public InputAction OnMenuOpen;
    public InputAction OnMenuClose;
    public InputAction OnInteract;
}
