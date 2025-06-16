using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBee : SkillController
{
    [SerializeField]
    private GameObject[] flys;

    private const int MaxFlysCount = 8; // this is for check prefab
    protected override void Awake()
    {
        skill = Defines.Skill.ShieldBee;
        base.Awake();

        StateSkillFollow state = stateMachine.RegisterState<StateSkillFollow>(Defines.State.Follow, this);
        state.WaitAfterAction += () => { State = Defines.State.Move; };
        stateMachine.RegisterState<StateSkillMove>(Defines.State.Move, this);

        for (int i = 0; i < flys.Length; ++i)
        {
            flys[i].SetActive(i < CreateCount);
        }
    }
    protected override void OnEnable()
    {
        base.OnEnable();
     
        State = Defines.State.Follow;
    }

    protected override void Update()
    {
        base.Update();
    }
}
