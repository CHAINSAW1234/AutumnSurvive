using UnityEngine;

public class Butterfly : SkillController
{
    protected override void Awake()
    {
        base.Awake();
        skill = Defines.Skill.Butterfly;
        stateMachine.RegisterState<StateSkillMove>(Defines.State.Move, this);
        Direction = new Vector2(0f, 1f);
        MoveSpeed = 5f;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        State = Defines.State.Move;
    }

}
