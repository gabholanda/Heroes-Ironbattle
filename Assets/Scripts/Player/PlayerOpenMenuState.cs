using System;
using UnityEngine.InputSystem;

public class PlayerOpenMenuState : BaseState
{

    private readonly PlayerStateMachine _sm;

    public PlayerOpenMenuState(PlayerStateMachine stateMachine) : base("OpenMenu", stateMachine)
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

    private void OnMenuClose(InputAction.CallbackContext obj)
    {
        _sm.RemoveState("OpenMenu");
        _sm.AddState(_sm.controlsState);
    }

    private void RegisterMenuEvent()
    {
        _sm.playerReader.OnMenuClose.performed += OnMenuClose;
    }

    private void UnregisterMenuEvent()
    {
        _sm.playerReader.OnMenuClose.performed -= OnMenuClose;
    }

    private void EnableMenuEvent()
    {
        _sm.playerReader.OnMenuClose.Enable();
    }

    private void DisableMenuEvent()
    {
        _sm.playerReader.OnMenuClose.Disable();
    }
}
