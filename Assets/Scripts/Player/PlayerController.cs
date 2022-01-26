using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private CharacterMovement characterMovement;
    [SerializeField]
    private CharacterCombat characterCombat;
    [SerializeField]
    private CharacterAnimator characterAnimator;
    [SerializeField]
    private CharacterStats stats;
    [SerializeField]
    private ParticleSystem castingParticles;
    [SerializeField]
    private AbilityHandler[] handlers;

    public InputReader PlayerReader;

    public GameObject castingPoint;

    public GameObject target;

    private void Start()
    {
        handlers[0].Initialize(castingPoint, new Vector2());
        characterAnimator = gameObject.AddComponent<CharacterAnimator>();
        characterMovement = gameObject.AddComponent<CharacterMovement>();
        characterMovement.CharAnim = characterAnimator;
        characterCombat = gameObject.AddComponent<CharacterCombat>();
        characterMovement.SetStats(stats);
        castingParticles.Stop();
    }
    private void Awake()
    {
        PlayerReader.OnMove.performed += OnMove;
        PlayerReader.OnFire.performed += OnFire;
        PlayerReader.OnAbilitySelect.performed += OnAbilitySelect;
        PlayerReader.OnAbilityCancel.performed += OnAbilityCancel;
        PlayerReader.OnDash.performed += OnDash;
        PlayerReader.OnMenuOpen.performed += OnMenuOpen;
        PlayerReader.OnMenuClose.performed += OnMenuClose;
    }

    private void OnMenuClose(InputAction.CallbackContext obj)
    {
        throw new NotImplementedException();
    }

    private void OnMenuOpen(InputAction.CallbackContext obj)
    {
        throw new NotImplementedException();
    }

    private void OnDash(InputAction.CallbackContext ctx)
    {
        characterMovement.Dash();
    }

    private void OnAbilitySelect(InputAction.CallbackContext obj)
    {
        castingParticles.Play();
    }

    private void OnAbilityCancel(InputAction.CallbackContext obj)
    {
        castingParticles.Stop();
    }

    private void OnFire(InputAction.CallbackContext obj)
    {
        //TODO: Add selected handler instead of first in list
        if (!handlers[0].isCoolingDown)
        {
            Vector3 mousePos = Mouse.current.position.ReadValue();
            mousePos.z = 10;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
            characterAnimator.SetAnimation("Casting", true, false);
            handlers[0].Execute(gameObject, worldPosition);
        }
    }

    private void OnMove(InputAction.CallbackContext ctx)
    {
        Vector2 v2 = ctx.ReadValue<Vector2>();
        characterMovement.SetVector(v2);
    }

    void OnEnable()
    {
        PlayerReader.OnMove.Enable();
        PlayerReader.OnFire.Enable();
        PlayerReader.OnAbilitySelect.Enable();
        PlayerReader.OnAbilityCancel.Enable();
        PlayerReader.OnDash.Enable();
        PlayerReader.OnMenuOpen.Enable();
        PlayerReader.OnMenuClose.Enable();
    }

    void OnDisable()
    {
        PlayerReader.OnMove.Disable();
        PlayerReader.OnFire.Disable();
        PlayerReader.OnAbilitySelect.Disable();
        PlayerReader.OnAbilityCancel.Disable();
        PlayerReader.OnDash.Disable();
        PlayerReader.OnMenuOpen.Disable();
        PlayerReader.OnMenuClose.Disable();
    }
}
