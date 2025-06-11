using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class State
{
    protected StateMachine stateMachine;

    public State(StateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public virtual void Enter()
    {

    }

    public virtual void Execute()
    {

    }

    public virtual void Exit()
    {

    }
}

public class StateDefault : State
{
    public StateDefault(StateMachine stateMachine, MonoBehaviour context) : base(stateMachine)
    { }
}