using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightBee : SkillController
{
    [SerializeField]
    private GameObject[] flys;
    private int createCount = 7;

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
        //createCount = // from gameManager;

        for (int i = 0; i < createCount; ++i)
        {
            flys[i].SetActive(true);
        }

        for (int i = createCount; i < flys.Length; ++i)
        {
            flys[i].SetActive(false);
        }

        float vertexExtent = Camera.main.orthographicSize;
        float horizontalExtent = vertexExtent * Screen.width / Screen.height;

        gameObject.transform.position = new Vector3(0, -vertexExtent - 0.5f, 0);
    }
    protected override void OnEnable()
    {
        base.OnEnable();
     
        State = Defines.State.Move;
    }

    protected override void Update()
    {
        base.Update();
    }
}
