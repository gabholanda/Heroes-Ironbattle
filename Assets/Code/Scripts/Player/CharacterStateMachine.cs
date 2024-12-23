﻿using UnityEngine;
using System.Collections.Generic;

public class CharacterStateMachine : StateMachine
{
    [HideInInspector]
    public PlayerControlsState controlsState;
    [HideInInspector]
    public PlayerClosedMenuState closedMenuState;
    [HideInInspector]
    public PlayerOpenMenuState openMenuState;
    [HideInInspector]
    public PlayerCloseStatsTabState closedTabState;
    [HideInInspector]
    public PlayerOpenStatsTabState openTabState;


    [HideInInspector]
    public CharacterEvent eventManager;
    [HideInInspector]
    public CharacterMovement movement;
    [HideInInspector]
    public CharacterCombat combat;
    [HideInInspector]
    public CharacterAnimator animator;
    [HideInInspector]
    public CharacterInteractor interactor;
    [HideInInspector]
    public CharacterUIAbilityManager abilityUI;

    [Header("Events")]
    public GameEvent OnFinishSetupCharacter;
    public GameEvent OnPlayerDeath;
    public GameEvent OnOpenMenu;
    public GameEvent OnCloseMenu;
    public GameEvent OnOpenStatsTab;
    public GameEvent OnCloseStatsTab;
    public List<GameEvent> CharacterEvents;
    [SerializeField]
    private List<Rarity> rarities;

    [SerializeField]
    private DashHandler dashHandler;

    [Header("Input")]
    public InputReader reader;


    [Header("Casting")]

    [SerializeField]
    public ParticleSystem castingParticles;

    [Header("UI Elements")]
    public SlidingBar manaBar;
    public SlidingBar healthBar;
    public GameObject menu;

    private void Awake()
    {
        InitializeStates();
        InitializeCharacter();
        InitializeAbilities();
        castingParticles.Stop();
        animator.SetAnimation("Idle", true, true, false, true);
        OnFinishSetupCharacter?.Raise();
    }

    private new void Update()
    {
        foreach (BaseState state in states.Values)
        {
            state.UpdateLogic();
        }
    }

    private new void FixedUpdate()
    {
        foreach (BaseState state in states.Values)
        {
            state.UpdatePhysics();
        }
    }

    public void Restore()
    {
        isDead = false;
        movement.enabled = true;
        movement.SetVector(new Vector2(0, 0));
        AddDefaultStates();
        SetStats();
        StartInitialStates();
        InitializeEvents();
        healthBar.UpdateBar(stats.resources.CurrentHealth / stats.resources.MaxHealth);
        manaBar.UpdateBar(stats.resources.CurrentMana / stats.resources.MaxMana);
    }

    public void CloseSettings()
    {
        int children = menu.transform.childCount;

        for (int i = 0; i < children; i++)
        {
            menu.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    private void InstantiateDefaultStates()
    {
        controlsState = new PlayerControlsState(this);
        closedMenuState = new PlayerClosedMenuState(this);
        openMenuState = new PlayerOpenMenuState(this);
        openTabState = new PlayerOpenStatsTabState(this);
        closedTabState = new PlayerCloseStatsTabState(this);
    }
    private void AddDefaultStates()
    {
        AddState(controlsState);
        AddState(closedMenuState);
        AddState(closedTabState);
    }

    private void StartInitialStates()
    {
        foreach (string stateName in states.Keys)
        {
            EnterState(stateName);
        }
    }

    private void InitializeStates()
    {
        CreateStateDictionary();
        InstantiateDefaultStates();
        AddDefaultStates();
        StartInitialStates();
    }

    private new void InitializeAbilities()
    {
        for (int i = 0; i < handlersSchema.Count; i++)
        {
            handlers[i] = Instantiate(handlersSchema[i]);
            handlersSchema[i].DeepCopy(handlers[i]);
        }

        for (int i = 0; i < handlers.Count; i++)
        {
            handlers[i].Initialize(castingPoint);
        }
        abilityUI
            .SetAbilityHandlers(handlers)
            .SetHandlers();
    }

    private void SetComponents()
    {
        animator = gameObject.AddComponent<CharacterAnimator>();
        movement = gameObject.AddComponent<CharacterMovement>();
        combat = gameObject.AddComponent<CharacterCombat>();
        interactor = gameObject.AddComponent<CharacterInteractor>();
        abilityUI = gameObject.AddComponent<CharacterUIAbilityManager>();
        eventManager = gameObject.AddComponent<CharacterEvent>();
    }

    private void InitializeEvents()
    {
        eventManager
            .InitializeEvents(CharacterEvents)
            .AddListeners();
    }

    private void InitializeCharacterMovement()
    {
        movement
            .SetAnimator(animator)
            .SetDashHandler(dashHandler)
            .InitializeDashHandler(castingPoint);
    }

    private void InitializeCharacterCombat()
    {
        combat
            .SetResources(stats.resources)
            .SetManaBar(manaBar);
    }

    private new void InitializeRegenerator()
    {
        healthRegenerator = GetComponent<HealthRegenerator>();
        healthRegenerator
            .SetResources(stats.resources)
            .SetManaBar(healthBar);
        healthRegenerator.StartRegeneration();

        manaRegenerator = GetComponent<ManaRegenerator>();
        manaRegenerator
            .SetResources(stats.resources)
            .SetManaBar(manaBar);
        manaRegenerator.StartRegeneration();
    }

    private void InitializeStats()
    {
        stats = new CharacterStats();
    }

    private void SetStats()
    {
        stats.SetCharacterStats(baseStats);
    }

    private void InitializeCharacter()
    {
        InitializeStats();
        SetStats();
        SetComponents();
        InitializeEvents();
        InitializeCharacterMovement();
        InitializeCharacterCombat();
        InitializeRegenerator();
    }
}
