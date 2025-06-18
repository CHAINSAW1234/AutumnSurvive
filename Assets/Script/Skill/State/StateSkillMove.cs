using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class StateSkillMove : State
{
    private SkillController context = null;

    private readonly Vector3 Direction = new Vector3(0f, 1f, 0f);

    public StateSkillMove(StateMachine stateMachine, SkillController context) : base(stateMachine)
    {
        this.context = context;
    }
    public override void Execute()
    {
        context.transform.position = context.transform.position + Direction * Time.deltaTime * context.MoveSpeed;
    }
}
