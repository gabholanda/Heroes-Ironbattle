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

    [SerializeField]
    public ParticleSystem castingParticles;

    public AbilityHandler[] handlers;
    [SerializeField]
    private DashHandler dashHandler;
    public InputReader playerReader;

    public GameObject castingPoint;

    private void Awake()
    {
        CreateStateDictionary();
        InstantiateDefaultStates();
        AddDefaultStates();
        StartInitialStates();
        InitializeAbilities();
        SetComponents();
        stats = new CharacterStats();
        InitializeCharacterMovement();

        stats.SetCharacterStats(baseStats);
        castingParticles.Stop();
    }

    private new void Update()
    {
        foreach (BaseState state in states.Values)
        {
            state.UpdateLogic();
        }
    }

    private new void LateUpdate()
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
    }

    private void InitializeCharacterMovement()
    {
        characterMovement
            .SetStats(stats)
            .SetAnimator(characterAnimator)
            .SetDashHandler(dashHandler)
            .InitializeDashHandler(castingPoint, gameObject.transform.position);
    }
}
