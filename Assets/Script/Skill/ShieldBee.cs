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
        base.Awake();
        StateSkillFollow state = stateMachine.RegisterState<StateSkillFollow>(Defines.State.Follow, this);
        state.WaitAfterAction += () => { State = Defines.State.Move; };
        stateMachine.RegisterState<StateSkillMove>(Defines.State.Move, this);

        if(flys.Length != MaxFlysCount)
        {
            Debug.Log("ShieldFly Prefab's list is not filled");
        }

        skill = Defines.Skill.ShieldBee;
        Direction = new Vector2(0f, 1f);
        CreateCount = 8;
        Duration = 5.5f;  // from Script
        MoveSpeed = 10f;

        for (int i = 0; i < CreateCount; ++i)
        {
            flys[i].SetActive(true);
        }

        for (int i = CreateCount; i < flys.Length; ++i)
        {
            flys[i].SetActive(false);
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
