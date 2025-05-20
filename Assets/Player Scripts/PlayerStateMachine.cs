using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerStateMachine
{
    public PlayerState currentState { get; private set; }//定义玩家当前状态   
    public void Initialize(PlayerState _startState) //初始化玩家的状态
    { 
        currentState = _startState;
        currentState.Enter();
    }
    public void ChangeState(PlayerState _newState)//改变玩家状态
    {
        currentState.Exit();
        currentState = _newState;
        currentState.Enter();
    }
}
