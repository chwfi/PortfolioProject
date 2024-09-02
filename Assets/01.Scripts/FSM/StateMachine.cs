using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateTypeEnum
{
    Idle,
    Move,
    Attack,
    Roll,
    Hit,
}

public class StateMachine
{
    public State CurrentState { get; private set; }

    public Dictionary<StateTypeEnum, State> StateDictionary { get; private set; }

    public StateMachine()
    {
        StateDictionary = new Dictionary<StateTypeEnum, State>();
    }

    public void Init(StateTypeEnum state)
    {
        CurrentState = StateDictionary[state];
        CurrentState.EnterState();
    }

    public void ChangeState(StateTypeEnum newState)
    {
        CurrentState.ExitState();
        CurrentState = StateDictionary[newState];
        CurrentState.EnterState();
    }

    public void AddState(StateTypeEnum stateType, State state)
    {
        StateDictionary.Add(stateType, state);
    }
}
