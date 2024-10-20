using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControlsState : BaseState
{

    private readonly CharacterStateMachine _sm;

    public PlayerControlsState(CharacterStateMachine stateMachine) : base("Controls", stateMachine)
    {
        _sm = stateMachine;
    }

    public override void Enter()
    {
        RegisterControlEvents();
        EnableControlEvents();
    }

    public override void Exit()
    {
        UnregisterControlEvents();
        DisableControlEvents();
    }

    private void OnInteract(InputAction.CallbackContext ctx)
    {
        _sm.interactor.Interact(_sm.gameObject);
    }


    private void OnDash(InputAction.CallbackContext ctx)
    {
        if (_sm.movement.IsMoving() && !_sm.movement.dashHandler.isCoolingDown)
        {
            _sm.movement.Dash();
            _sm.eventManager.events["Dash"].Raise();
        }
    }

    private void OnAbilitySelect(InputAction.CallbackContext obj)
    {
        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            _sm.combat.SelectAbility(_sm.handlers[0]);
        }
        else if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            _sm.combat.SelectAbility(_sm.handlers[1]);
        }
        else if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            _sm.combat.SelectAbility(_sm.handlers[2]);
        }
        else if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            _sm.combat.SelectAbility(_sm.handlers[3]);
        }
        _sm.castingParticles.Play();
    }

    public void OnAbilityCancel(InputAction.CallbackContext obj)
    {
        _sm.combat.CancelAbilitySelection();
        _sm.castingParticles.Stop();
    }

    public void OnFire(InputAction.CallbackContext obj)
    {
        if (_sm.combat.CanCast())
        {
            // TODO: fazer isso em um script de utility;
            Vector3 mousePos = Mouse.current.position.ReadValue();
            mousePos.z = 10;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
            _sm.animator.SetAnimation("Casting", true, false, true, true);
            _sm.combat.Cast(_sm.gameObject, worldPosition);
            _sm.eventManager.events["Cast"].Raise();
        }
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        Vector2 v2 = ctx.ReadValue<Vector2>();
        _sm.movement.SetVector(v2 * _sm.stats.combatStats.MoveSpeed);
    }

    private void RegisterControlEvents()
    {
        _sm.reader.OnMove.performed += OnMove;
        _sm.reader.OnFire.performed += OnFire;
        _sm.reader.OnAbilitySelect.performed += OnAbilitySelect;
        _sm.reader.OnAbilityCancel.performed += OnAbilityCancel;
        _sm.reader.OnDash.performed += OnDash;
        _sm.reader.OnInteract.performed += OnInteract;
    }

    private void EnableControlEvents()
    {
        _sm.reader.OnMove.Enable();
        _sm.reader.OnFire.Enable();
        _sm.reader.OnAbilitySelect.Enable();
        _sm.reader.OnAbilityCancel.Enable();
        _sm.reader.OnDash.Enable();
        _sm.reader.OnInteract.Enable();
    }

    private void UnregisterControlEvents()
    {
        _sm.reader.OnMove.performed -= OnMove;
        _sm.reader.OnFire.performed -= OnFire;
        _sm.reader.OnAbilitySelect.performed -= OnAbilitySelect;
        _sm.reader.OnAbilityCancel.performed -= OnAbilityCancel;
        _sm.reader.OnDash.performed -= OnDash;
        _sm.reader.OnInteract.performed -= OnInteract;
    }

    private void DisableControlEvents()
    {
        _sm.reader.OnMove.Disable();
        _sm.reader.OnFire.Disable();
        _sm.reader.OnAbilitySelect.Disable();
        _sm.reader.OnAbilityCancel.Disable();
        _sm.reader.OnDash.Disable();
        _sm.reader.OnInteract.Disable();
    }
}
