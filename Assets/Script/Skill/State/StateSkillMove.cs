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
    public StateSkillMove(StateMachine stateMachine, SkillController context) : base(stateMachine)
    {
        this.context = context;
    }
    public override void Execute()
    {
        context.transform.position = context.transform.position + new Vector3(context.Direction.x, context.Direction.y, 0) * Time.deltaTime * context.MoveSpeed;
    }
}
