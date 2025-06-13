using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightBee : SkillController
{
    [SerializeField]
    private GameObject[] flys;

    private const int FlysCount = 7;  // this is for check prefab
    protected override void Awake()
    {
        base.Awake();
        stateMachine.RegisterState<StateSkillMove>(Defines.State.Move, this);

        if(flys.Length != FlysCount)
        {
            Debug.Log("StraightBee Prefab's list is not filled");
        }

        skill = Defines.Skill.StraightBee;
        Direction = new Vector2(0f, 1f);
        CreateCount = 7;
        MoveSpeed = 10f;
        //createCount = // from gameManager;

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
     
        State = Defines.State.Move;

        float vertexExtent = Camera.main.orthographicSize;
        float horizontalExtent = vertexExtent * Screen.width / Screen.height;
        gameObject.transform.position = new Vector3(0, -vertexExtent - 0.5f, 0);
    }

    protected override void Update()
    {
        base.Update();
    }
}
