using UnityEngine;

public abstract class SkillController : MonoBehaviour
{
    protected StateMachine stateMachine = new StateMachine();

    public Transform Player { get; protected set; } = null;

    protected Defines.Skill skill;
    public float MoveSpeed { get; protected set; }
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
        
        var data = Managers.Data.GetSkillLevelDataHelper(skill, PlayerDataController.Instance.GetSkillLevelAt(skill));
        if (data.HasValue)
        {
            MoveSpeed = data.Value.moveSpeed;
            Duration = data.Value.duration/10;
            CreateCount = data.Value.createCount;
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.activeInHierarchy == false)
        {
            return;
        }

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
