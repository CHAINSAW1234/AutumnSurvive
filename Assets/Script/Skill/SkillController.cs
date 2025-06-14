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
        SetStats();
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

    protected abstract void SetStats();

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
