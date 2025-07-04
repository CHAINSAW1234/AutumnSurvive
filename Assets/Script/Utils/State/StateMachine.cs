using System;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    private Dictionary<Defines.State, State> states = new Dictionary<Defines.State, State>();
    private State curState = null;
    private Defines.State curStateType = Defines.State.END;

    public State CurState { get { return curState; } }
    public Defines.State CurStateType { get { return curStateType; } set { ChangeState(value); } }

    public T RegisterState<T>(Defines.State stateType, MonoBehaviour context) where T : State
    {
        T state = Activator.CreateInstance(typeof(T), this, context) as T;
        states[stateType] = state;

        return state;
    }

    public T GetState<T>(Defines.State stateType) where T : State
    {
        if (states.ContainsKey(stateType) && states[stateType] is T)
        {
            return states[stateType] as T;
        }

        return null;
    }

    public void ChangeState(Defines.State stateType)
    {
        if (!states.ContainsKey(stateType))
        {
            Debug.Log($"Change Undefined State {stateType.ToString()}");
            return;
        }

        if (curState == states[stateType])
        {
            return;
        }

        State preState = curState;
        curState = states[stateType];
        curStateType = stateType;

        if (preState != null)
        {
            preState.Exit();
        }

        if (curState != null)
        {
            curState.Enter();
        }
    }

    public void Execute()
    {
        if (curState != null)
        {
            curState.Execute();
        }
    }
}

