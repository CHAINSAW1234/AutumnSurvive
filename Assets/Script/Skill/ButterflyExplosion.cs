using UnityEngine;

public class ButterflyExplosion : SkillController
{
    protected override void Awake()
    {
        base.Awake();
        stateMachine.RegisterState<StateSkillAppear>(Defines.State.Appear, this);
        stateMachine.RegisterState<StateSkillWait>(Defines.State.Wait, this);
        stateMachine.RegisterState<StateSkillDisappear>(Defines.State.Disappear, this);
        Duration = 7.5f;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        skill = Defines.Skill.ButterflyExplosion;
        State = Defines.State.Appear;
    }

}
