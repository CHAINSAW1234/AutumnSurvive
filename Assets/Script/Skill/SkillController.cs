using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class SkillController : MonoBehaviour
{
    protected StateMachine stateMachine = new StateMachine();
    public Transform Player { get; protected set; } = null;

    public Defines.Skill skill;

    public float MoveSpeed { get; protected set; }
    public Vector2 Direction { get; protected set; }  = Vector2.zero;
    public float Duration { get; protected set; } = Defines.Infinity;
    public int CreateCount { get; protected set; } = 0;

    public Defines.State State
    {
        get { return stateMachine.CurStateType; }
        set { stateMachine.CurStateType = value; }
    }

    protected virtual void Awake()
    {
        Player = GameObject.FindWithTag("Player").transform;
        stateMachine.RegisterState<StateDefault>(Defines.State.END, this);
        State = Defines.State.END;
        SetStatsFromData();
    }

    protected virtual void OnEnable()
    {
    }

    protected virtual void Update()
    {
        stateMachine.Execute();
    }

    protected virtual void OnDisable()
    {
        State = Defines.State.END;
    }

    protected void SetStatsFromData()
    {
        var data = Managers.Data.GetSkillLevelDataHelper(skill, 3);
        if(data.HasValue)
        {
            MoveSpeed = data.Value.moveSpeed;
            Direction = data.Value.direction.ToVector2();
            Duration = data.Value.duration/10;
            CreateCount = data.Value.createCount;
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.transform.tag)
        {
            case "Boundary":
                CollisionBoundary(collision);
                break;
        }
    }

    private void CollisionBoundary(Collision2D collision)
    {
        Managers.Resource.Destroy(gameObject);
    }

}
