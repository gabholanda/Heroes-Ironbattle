using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    [HideInInspector]
    public PlayerControlsState controlsState;
    [HideInInspector]
    public PlayerClosedMenuState closedMenuState;
    [HideInInspector]
    public PlayerOpenMenuState openMenuState;


    [HideInInspector]
    public CharacterMovement characterMovement;
    [HideInInspector]
    public CharacterCombat characterCombat;
    [HideInInspector]
    public CharacterAnimator characterAnimator;
    [HideInInspector]
    public CharacterInteractor characterInteractor;
    [HideInInspector]
    public CharacterUIAbilityManager abilityUI;

    [Header("Abilities")]
    public AbilityHandler[] handlers;
    [SerializeField]
    private DashHandler dashHandler;

    [Header("Input")]
    public InputReader playerReader;


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
        characterAnimator.SetAnimation("Idle", true, true, false, true);
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
            handlers[i].Initialize(castingPoint, new Vector2());
        }
        abilityUI
            .SetAbilityHandlers(handlers)
            .SetHandlers();
    }

    private void SetComponents()
    {
        characterAnimator = gameObject.AddComponent<CharacterAnimator>();
        characterMovement = gameObject.AddComponent<CharacterMovement>();
        characterCombat = gameObject.AddComponent<CharacterCombat>();
        characterInteractor = gameObject.AddComponent<CharacterInteractor>();
        abilityUI = gameObject.AddComponent<CharacterUIAbilityManager>();
    }

    private void InitializeCharacterMovement()
    {
        characterMovement
            .SetStats(stats)
            .SetAnimator(characterAnimator)
            .SetDashHandler(dashHandler)
            .InitializeDashHandler(castingPoint, gameObject.transform.position);
    }

    private void InitializeCharacterCombat()
    {
        characterCombat
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
        SetComponents();
        stats = new CharacterStats();
        InitializeCharacterMovement();
        stats.SetCharacterStats(baseStats);
        InitializeCharacterCombat();
        InitializeRegenerator();
    }
}
