using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerOpenMenuState : BaseState
{

    private readonly CharacterStateMachine _sm;

    public PlayerOpenMenuState(CharacterStateMachine stateMachine) : base("OpenMenu", stateMachine)
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
        _sm.OnCloseMenu.Raise();
        _sm.RemoveState("OpenMenu");
        _sm.AddState(_sm.closedMenuState);
        _sm.closedMenuState.Enter();
    }

    private void RegisterMenuEvent()
    {
        _sm.reader.TriggerMenu.performed += OnMenuClose;
    }

    private void UnregisterMenuEvent()
    {
        _sm.reader.TriggerMenu.performed -= OnMenuClose;
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
