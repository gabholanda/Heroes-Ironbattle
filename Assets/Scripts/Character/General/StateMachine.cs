﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    [SerializeField]
    protected CharacterBaseStats baseStats;
    protected BaseState currentState;
    public Dictionary<string, BaseState> states;
    public CharacterStats stats;
    public bool isDead;

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

    protected void LateUpdate()
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
}