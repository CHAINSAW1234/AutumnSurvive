using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class StateSkillWait : State
{
    private SkillController context = null;
    private Coroutine coroutine = null;
    public StateSkillWait(StateMachine stateMachine, SkillController context) : base(stateMachine)
    {
        this.context = context;
    }

    public override void Enter()
    {
        coroutine = context.StartCoroutine(IEnumerators.WaitForDurationAndAction(context.Duration / 10,
            () => { context.State = Defines.State.Disappear; }));
    }

    public override void Exit()
    {
        base.Exit();
        context.StopCoroutine(coroutine);
        coroutine = null;
    }
}

