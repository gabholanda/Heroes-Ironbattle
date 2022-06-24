using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControlsState : BaseState
{

    private readonly PlayerStateMachine _sm;

    public PlayerControlsState(PlayerStateMachine stateMachine) : base("Controls", stateMachine)
    {
        _sm = stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        RegisterControlEvents();
        EnableControlEvents();
    }

    public override void Exit()
    {
        base.Exit();
        UnregisterControlEvents();
        DisableControlEvents();
    }

    private void OnInteract(InputAction.CallbackContext ctx)
    {
        _sm.characterInteractor.Interact(_sm.gameObject);
    }


    private void OnDash(InputAction.CallbackContext ctx)
    {
        _sm.characterMovement.Dash();
    }

    private void OnAbilitySelect(InputAction.CallbackContext obj)
    {
        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            _sm.characterCombat.SelectAbility(_sm.handlers[0]);
        }
        else if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            _sm.characterCombat.SelectAbility(_sm.handlers[1]);
        }
        else if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            _sm.characterCombat.SelectAbility(_sm.handlers[2]);
        }
        else if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            _sm.characterCombat.SelectAbility(_sm.handlers[3]);
        }
        _sm.castingParticles.Play();
    }

    public void OnAbilityCancel(InputAction.CallbackContext obj)
    {
        _sm.characterCombat.CancelAbilitySelection();
        _sm.castingParticles.Stop();
    }

    public void OnFire(InputAction.CallbackContext obj)
    {
        if (_sm.characterCombat.CanCast())
        {
            // TODO: fazer isso em um script de utility;
            Vector3 mousePos = Mouse.current.position.ReadValue();
            mousePos.z = 10;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
            _sm.characterAnimator.SetAnimation("Casting", true, false, true, true);
            _sm.characterCombat.Cast(_sm.gameObject, worldPosition);
        }
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        Vector2 v2 = ctx.ReadValue<Vector2>();
        _sm.characterMovement.SetVector(v2);
    }

    private void RegisterControlEvents()
    {
        _sm.playerReader.OnMove.performed += OnMove;
        _sm.playerReader.OnFire.performed += OnFire;
        _sm.playerReader.OnAbilitySelect.performed += OnAbilitySelect;
        _sm.playerReader.OnAbilityCancel.performed += OnAbilityCancel;
        _sm.playerReader.OnDash.performed += OnDash;
        _sm.playerReader.OnInteract.performed += OnInteract;
    }

    private void EnableControlEvents()
    {
        _sm.playerReader.OnMove.Enable();
        _sm.playerReader.OnFire.Enable();
        _sm.playerReader.OnAbilitySelect.Enable();
        _sm.playerReader.OnAbilityCancel.Enable();
        _sm.playerReader.OnDash.Enable();
        _sm.playerReader.OnInteract.Enable();
    }

    private void UnregisterControlEvents()
    {
        _sm.playerReader.OnMove.performed -= OnMove;
        _sm.playerReader.OnFire.performed -= OnFire;
        _sm.playerReader.OnAbilitySelect.performed -= OnAbilitySelect;
        _sm.playerReader.OnAbilityCancel.performed -= OnAbilityCancel;
        _sm.playerReader.OnDash.performed -= OnDash;
        _sm.playerReader.OnInteract.performed -= OnInteract;
    }

    private void DisableControlEvents()
    {
        _sm.playerReader.OnMove.Disable();
        _sm.playerReader.OnFire.Disable();
        _sm.playerReader.OnAbilitySelect.Disable();
        _sm.playerReader.OnAbilityCancel.Disable();
        _sm.playerReader.OnDash.Disable();
        _sm.playerReader.OnInteract.Disable();
    }
}
