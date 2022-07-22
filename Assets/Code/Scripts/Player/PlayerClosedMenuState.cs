using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerClosedMenuState : BaseState
{

    private readonly CharacterStateMachine _sm;

    public PlayerClosedMenuState(CharacterStateMachine stateMachine) : base("ClosedMenu", stateMachine)
    {
        _sm = stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        RegisterMenuEvent();
        EnableMenuEvent();
    }

    public override void Exit()
    {
        base.Exit();
        UnregisterMenuEvent();
        DisableMenuEvent();
    }

    private void OnMenuOpen(InputAction.CallbackContext obj)
    {
        _sm.OpenSettings();
        _sm.RemoveState("ClosedMenu");
        _sm.AddState(_sm.openMenuState);
        _sm.openMenuState.Enter();
    }

    private void RegisterMenuEvent()
    {
        _sm.reader.TriggerMenu.performed += OnMenuOpen;
    }

    private void UnregisterMenuEvent()
    {
        _sm.reader.TriggerMenu.performed -= OnMenuOpen;
    }

    private void EnableMenuEvent()
    {
        _sm.reader.TriggerMenu.Enable();
    }

    private void DisableMenuEvent()
    {
        _sm.reader.TriggerMenu.Disable();
    }

}
