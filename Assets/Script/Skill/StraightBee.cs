using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightBee : SkillController
{
    [SerializeField]
    private GameObject[] flys;

    protected override void Awake()
    {
        skill = Defines.Skill.StraightBee;
        base.Awake();

        stateMachine.RegisterState<StateSkillMove>(Defines.State.Move, this);

        for (int i = 0; i < flys.Length; ++i)
        {
            flys[i].SetActive(i < CreateCount);
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
