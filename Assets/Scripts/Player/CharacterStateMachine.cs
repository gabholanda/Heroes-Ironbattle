using UnityEngine;

public class CharacterStateMachine : StateMachine
{
    [HideInInspector]
    public PlayerControlsState controlsState;
    [HideInInspector]
    public PlayerClosedMenuState closedMenuState;
    [HideInInspector]
    public PlayerOpenMenuState openMenuState;

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

    public ArtifactInventory inventory;

    [Header("Abilities")]
    public AbilityHandler[] handlers;
    [SerializeField]
    private DashHandler dashHandler;

    [Header("Input")]
    public InputReader reader;


    [Header("Casting")]

    [SerializeField]
    public ParticleSystem castingParticles;
    public GameObject castingPoint;

    [Header("UI Elements")]
    public SlidingBar manaBar;
    public GameObject menu;

    private ManaRegenerator manaRegenerator;
    private void Awake()
    {
        menu = GameObject.FindGameObjectWithTag("Settings");
        InitializeStates();
        InitializeCharacter();
        InitializeAbilities();
        castingParticles.Stop();
        animator.SetAnimation("Idle", true, true, false, true);
        if (inventory is null) inventory = ScriptableObject.CreateInstance<ArtifactInventory>();
        inventory.Items.ForEach(inventoryItem => inventoryItem.Item.Apply(gameObject));
        inventory.holder = gameObject;
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

    public void OpenSettings()
    {
        int children = menu.transform.childCount;

        for (int i = 0; i < children; i++)
        {
            menu.transform.GetChild(i).gameObject.SetActive(true);
        }
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
    }
    private void AddDefaultStates()
    {
        AddState(controlsState);
        AddState(closedMenuState);
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

    private void InitializeAbilities()
    {
        for (int i = 0; i < handlers.Length; i++)
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
            .InitializeEvents()
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

    private void InitializeRegenerator()
    {
        manaRegenerator = GetComponent<ManaRegenerator>();
        manaRegenerator
            .SetResources(stats.resources)
            .SetManaBar(manaBar);
        manaRegenerator.StartRegeneration();
    }

    private void InitializeCharacter()
    {
        stats = new CharacterStats();
        stats.SetCharacterStats(baseStats);
        SetComponents();
        InitializeEvents();
        InitializeCharacterMovement();
        InitializeCharacterCombat();
        InitializeRegenerator();
    }
}
