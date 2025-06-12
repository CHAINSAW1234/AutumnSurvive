using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class StateSkillFollow : State
{
    private SkillController context = null;
    private readonly List<Coroutine> coroutines = new List<Coroutine>();
    public Action WaitAfterAction { get; set; }
    public StateSkillFollow(StateMachine stateMachine, SkillController context) : base(stateMachine)
    {
        this.context = context;
    }

    public override void Enter()
    {
        coroutines.Add(context.StartCoroutine(IEnumerators.WaitForDurationAndAction(context.Duration, WaitAfterAction)));
        coroutines.Add(context.StartCoroutine(FollowPlayer()));
    }

    public override void Exit()
    {
        foreach (Coroutine coroutine in coroutines)
        {
            context.StopCoroutine(coroutine);
        }
        coroutines.Clear();
    }


    private IEnumerator FollowPlayer()
    {
        while (true)
        {
            context.transform.position = context.Player.position;
            yield return null;
        }
    }
}

