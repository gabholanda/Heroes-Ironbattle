using System;
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
        _sm.RemoveState("Controls");
        _sm.AddState(_sm.openMenuState);
    }

    private void RegisterMenuEvent()
    {
        _sm.playerReader.OnMenuOpen.performed += OnMenuOpen;
    }

    private void UnregisterMenuEvent()
    {
        _sm.playerReader.OnMenuOpen.performed -= OnMenuOpen;
    }

    private void EnableMenuEvent()
    {
        _sm.playerReader.OnMenuOpen.Enable();
    }

    private void DisableMenuEvent()
    {
        _sm.playerReader.OnMenuOpen.Disable();
    }

}
