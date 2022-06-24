using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerClosedMenuState : BaseState
{

    private readonly PlayerStateMachine _sm;

    public PlayerClosedMenuState(PlayerStateMachine stateMachine) : base("ClosedMenu", stateMachine)
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
        _sm.playerReader.TriggerMenu.performed += OnMenuOpen;
    }

    private void UnregisterMenuEvent()
    {
        _sm.playerReader.TriggerMenu.performed -= OnMenuOpen;
    }

    private void EnableMenuEvent()
    {
        _sm.playerReader.TriggerMenu.Enable();
    }

    private void DisableMenuEvent()
    {
        _sm.playerReader.TriggerMenu.Disable();
    }

}
