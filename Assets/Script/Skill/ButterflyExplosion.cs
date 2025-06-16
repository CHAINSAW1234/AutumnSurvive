using UnityEngine;

public class ButterflyExplosion : SkillController
{
    protected override void Awake()
    {
        skill = Defines.Skill.ButterflyExplosion;
        base.Awake();

        stateMachine.RegisterState<StateSkillAppear>(Defines.State.Appear, this);
        stateMachine.RegisterState<StateSkillWait>(Defines.State.Wait, this);
        stateMachine.RegisterState<StateSkillDisappear>(Defines.State.Disappear, this);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        State = Defines.State.Appear;
    }

}
