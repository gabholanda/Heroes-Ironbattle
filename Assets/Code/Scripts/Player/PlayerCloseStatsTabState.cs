using UnityEngine.InputSystem;

public class PlayerCloseStatsTabState : BaseState
{

    private readonly CharacterStateMachine _sm;

    public PlayerCloseStatsTabState(CharacterStateMachine stateMachine) : base("ClosedStatsTab", stateMachine)
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

    private void OnStatsTabOpen(InputAction.CallbackContext obj)
    {
        _sm.OnOpenStatsTab.Raise();
        _sm.RemoveState("ClosedStatsTab");
        _sm.AddState(_sm.openTabState);
        _sm.openTabState.Enter();
    }

    private void RegisterMenuEvent()
    {
        _sm.reader.TriggerStatsTab.performed += OnStatsTabOpen;
    }

    private void UnregisterMenuEvent()
    {
        _sm.reader.TriggerStatsTab.performed -= OnStatsTabOpen;
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
