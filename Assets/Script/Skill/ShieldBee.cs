using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBee : SkillController
{
    [SerializeField]
    private GameObject[] flys;
    private int createCount = 8;

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

        skill = Defines.Skill.ShelidBee;
        Direction = new Vector2(0f, 1f);
        //createCount = // from gameManager;
        Duration = 55;  // from Script

        for (int i = 0; i < createCount; ++i)
        {
            flys[i].SetActive(true);
        }

        for (int i = createCount; i < flys.Length; ++i)
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
