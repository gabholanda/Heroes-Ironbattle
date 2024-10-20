using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthRegenerator), typeof(ManaRegenerator))]
public class StateMachine : MonoBehaviour
{
    [SerializeField]
    protected CharacterBaseStats baseStats;
    protected BaseState currentState;
    public Dictionary<string, BaseState> states;
    public CharacterStats stats;
    public bool isDead;

    public GameObject castingPoint;
    protected ManaRegenerator manaRegenerator;
    protected HealthRegenerator healthRegenerator;

    [Header("Abilities")]
    public List<AbilityHandler> handlersSchema;
    [HideInInspector]
    public List<AbilityHandler> handlers;

    void Start()
    {
        currentState = GetInitialState();
        if (currentState != null)
            currentState.Enter();
    }

    protected void Update()
    {
        if (currentState != null)
            currentState.UpdateLogic();
    }

    protected void FixedUpdate()
    {
        if (currentState != null)
            currentState.UpdatePhysics();
    }

    public void ChangeState(BaseState newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }

    public void ChangeState(string stateName)
    {
        currentState.Exit();
        currentState = states[stateName];
        currentState.Enter();
    }

    public void AddState(BaseState newState)
    {
        states.Add(newState.name, newState);
    }

    protected void EnterState(string stateName)
    {
        states[stateName].Enter();
    }

    public void RemoveState(string stateName)
    {
        states[stateName].Exit();
        states.Remove(stateName);
    }

    public void CleanDead()
    {
        Destroy(gameObject);
    }

    protected void CreateStateDictionary()
    {
        states = new Dictionary<string, BaseState>();
    }

    protected virtual BaseState GetInitialState()
    {
        return null;
    }

    public virtual void InitializeAbilities()
    {
        for (int i = 0; i < handlersSchema.Count; i++)
        {
            handlers.Add(Instantiate(handlersSchema[i]));
            handlersSchema[i].DeepCopy(handlers[i]);
        }
        for (int i = 0; i < handlers.Count; i++)
        {
            handlers[i].Initialize(gameObject);
        }
    }
    public virtual void InitializeRegenerator()
    {
        healthRegenerator = GetComponent<HealthRegenerator>();
        healthRegenerator
            .SetResources(stats.resources);
        healthRegenerator.StartRegeneration();

        manaRegenerator = GetComponent<ManaRegenerator>();
        manaRegenerator
            .SetResources(stats.resources);
        manaRegenerator.StartRegeneration();
    }
}