using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class EnemyStateMachine
{
    public EnemyState currentState { get; private set; }
    public void Initialized(EnemyState _startState)
    {
        currentState = _startState;
        currentState.Enter();
    }
    public void ChangeState(EnemyState _newState)
    {
        currentState.Exit();
        currentState= _newState;
        currentState.Enter();
    }
}
