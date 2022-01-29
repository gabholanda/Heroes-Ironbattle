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
    [SerializeField]
    private DashHandler dashHandler;
    public InputReader playerReader;

    public GameObject castingPoint;

    public GameObject target;

    private void Start()
    {
        handlers[0].Initialize(castingPoint, new Vector2());

        characterAnimator = gameObject.AddComponent<CharacterAnimator>();
        characterMovement = gameObject.AddComponent<CharacterMovement>();

        characterMovement
            .SetStats(stats)
            .SetAnimator(characterAnimator)
            .SetDashHandler(dashHandler)
            .InitializeDashHandler(castingPoint, gameObject.transform.position);

        characterCombat = gameObject.AddComponent<CharacterCombat>();
        castingParticles.Stop();
    }
    private void Awake()
    {
        playerReader.OnMove.performed += OnMove;
        playerReader.OnFire.performed += OnFire;
        playerReader.OnAbilitySelect.performed += OnAbilitySelect;
        playerReader.OnAbilityCancel.performed += OnAbilityCancel;
        playerReader.OnDash.performed += OnDash;
        playerReader.OnMenuOpen.performed += OnMenuOpen;
        playerReader.OnMenuClose.performed += OnMenuClose;

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
        playerReader.OnMove.Enable();
        playerReader.OnFire.Enable();
        playerReader.OnAbilitySelect.Enable();
        playerReader.OnAbilityCancel.Enable();
        playerReader.OnDash.Enable();
        playerReader.OnMenuOpen.Enable();
        playerReader.OnMenuClose.Enable();
    }

    void OnDisable()
    {
        playerReader.OnMove.Disable();
        playerReader.OnFire.Disable();
        playerReader.OnAbilitySelect.Disable();
        playerReader.OnAbilityCancel.Disable();
        playerReader.OnDash.Disable();
        playerReader.OnMenuOpen.Disable();
        playerReader.OnMenuClose.Disable();
    }
}
