using UnityEngine.InputSystem;

public class PlayerOpenStatsTabState : BaseState
{

    private readonly CharacterStateMachine _sm;

    public PlayerOpenStatsTabState(CharacterStateMachine stateMachine) : base("OpenStatsTab", stateMachine)
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
        UnregisterMenuEvent();
        DisableMenuEvent();
    }

    private void OnStatsTabClose(InputAction.CallbackContext obj)
    {
        _sm.OnCloseStatsTab.Raise();
        _sm.RemoveState("OpenStatsTab");
        _sm.AddState(_sm.closedTabState);
        _sm.closedTabState.Enter();
    }

    private void RegisterMenuEvent()
    {
        _sm.reader.TriggerStatsTab.performed += OnStatsTabClose;
    }

    private void UnregisterMenuEvent()
    {
        _sm.reader.TriggerStatsTab.performed -= OnStatsTabClose;
    }

    private void EnableMenuEvent()
    {
        _sm.reader.TriggerStatsTab.Enable();
    }

    private void DisableMenuEvent()
    {
        _sm.reader.TriggerStatsTab.Disable();
    }
}
