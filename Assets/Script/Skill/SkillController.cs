using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class SkillController : MonoBehaviour
{
    protected Defines.Skill skill;
    protected StateMachine stateMachine = new StateMachine();

    public Transform Player { get; protected set; } = null;
    public float MoveSpeed { get; protected set; }
    public Vector2 Direction { get; protected set; }  = Vector2.zero;
    public float Duration { get; protected set; } = Defines.Infinity;
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

    private void Move()
    {
        transform.position = transform.position + new Vector3(Direction.x, Direction.y, 0) * Time.deltaTime * MoveSpeed;
    }
    private IEnumerator WaitForDurationAndAction(Action action)
    {
        yield return new WaitForSeconds(Duration / 10);
        action?.Invoke();
        yield break;
    }

    #region State

    public class StateSkillWait : State
    {
        private SkillController context = null;
        Coroutine coroutine = null;
        public StateSkillWait(StateMachine stateMachine, SkillController context) : base(stateMachine)
        {
            this.context = context;
        }

        public override void Enter()
        {
            coroutine = context.StartCoroutine(context.WaitForDurationAndAction(
                () => { context.State = Defines.State.Disappear; }));
        }

    }
    public class StateSkillFollow : State
    {
        private SkillController context = null;
        private readonly List<Coroutine> coroutines = new List<Coroutine>();
        public Action WaitAfterAction { get; set;}
        public StateSkillFollow(StateMachine stateMachine, SkillController context) : base(stateMachine)
        {
            this.context = context;
        }

        public override void Enter()
        {
            coroutines.Add(context.StartCoroutine(context.WaitForDurationAndAction(WaitAfterAction)));
            coroutines.Add(context.StartCoroutine(FollowPlayer()));
        }

        public override void Exit()
        {
            foreach( Coroutine coroutine in coroutines)
            {
                context.StopCoroutine(coroutine);
            }
            coroutines.Clear();
        }


        private IEnumerator FollowPlayer()
        {
            while(true)
            {
                context.transform.position = context.Player.position;
                yield return null;
            }
        }
    }

    public class StateSkillMove : State
    {
        SkillController context = null;
        public StateSkillMove(StateMachine stateMachine, SkillController context) : base(stateMachine)
        {
            this.context = context;
        }
        public override void Execute()
        {
            context.Move();
        }
    }

    public class StateSkillAppear : State
    {
        SkillController context = null;
        private Vector3 scale = Vector3.zero;

        private const float DeltaBias = 1.5f;
        public StateSkillAppear(StateMachine stateMachine, SkillController context) : base(stateMachine)
        {
            this.context = context;
            scale = context.transform.localScale;
        }

        public override void Enter()
        {
            context.transform.localScale = Vector3.zero;
        }
        public override void Execute()
        {
            context.transform.localScale = context.transform.localScale + scale * Time.deltaTime * DeltaBias;

            if(context.transform.localScale.x > scale.x)
            {
                context.transform.localScale = scale;
                context.State = Defines.State.Wait;
            }
        }
    }

    public class StateSkillDisappear : State
    {
        SkillController context = null;
        private Vector3 scale = Vector3.zero;

        private const float DeltaBias = 1.5f;

        public StateSkillDisappear(StateMachine stateMachine, SkillController context) : base(stateMachine)
        {
            this.context = context;
            scale = context.transform.localScale;
        }

        public override void Execute()
        {
            context.transform.localScale = context.transform.localScale - scale * Time.deltaTime * DeltaBias;

            if (context.transform.localScale.x < 0)
            {
                context.transform.localScale = Vector3.zero;
                Managers.Resource.Destroy(context.gameObject);
            }
        }

    }

    #endregion

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
