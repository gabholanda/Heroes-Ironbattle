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

    [SerializeField]
    public ParticleSystem castingParticles;

    public AbilityHandler[] handlers;
    [SerializeField]
    private DashHandler dashHandler;
    public InputReader playerReader;

    [SerializeField]
    public SlidingBar manaBar;

    public GameObject castingPoint;
    private ManaRegenerator manaRegenerator;

    private void Awake()
    {
        InitializeStates();
        InitializeAbilities();
        InitializeCharacter();
        castingParticles.Stop();
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

    void InitializeAbilities()
    {
        for (int i = 0; i < handlers.Length; i++)
        {
            handlers[i].Initialize(castingPoint, new Vector2());
        }
    }

    void SetComponents()
    {
        characterAnimator = gameObject.AddComponent<CharacterAnimator>();
        characterMovement = gameObject.AddComponent<CharacterMovement>();
        characterCombat = gameObject.AddComponent<CharacterCombat>();
        characterInteractor = gameObject.AddComponent<CharacterInteractor>();
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
            .SetStats(stats)
            .SetManaBar(manaBar);
    }

    private void InitializeRegenerator()
    {
        manaRegenerator = GetComponent<ManaRegenerator>();
        manaRegenerator
            .SetStats(stats)
            .SetManaBar(manaBar);
        manaRegenerator.StartRegeneration();
    }

    private void InitializeCharacter()
    {
        SetComponents();
        InitializeCharacterMovement();
        InitializeCharacterCombat();
        InitializeRegenerator();
    }
}
