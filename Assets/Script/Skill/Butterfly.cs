using UnityEngine;

public class Butterfly : SkillController
{
    protected override void Awake()
    {
        skill = Defines.Skill.Butterfly;
        base.Awake();

        stateMachine.RegisterState<StateSkillMove>(Defines.State.Move, this);

    }

    protected override void OnEnable()
    {
        base.OnEnable();
        State = Defines.State.Move;
        Managers.Sound.Play("Butterfly", Defines.Sound.Effect);
    }

}
