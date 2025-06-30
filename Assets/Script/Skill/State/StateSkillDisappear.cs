using UnityEngine;
public class StateSkillDisappear : State
{
    private SkillController context = null;
    private Vector3 scale = Vector3.zero;

    private const float deltaBias = 1.5f;

    public StateSkillDisappear(StateMachine stateMachine, SkillController context) : base(stateMachine)
    {
        this.context = context;
        scale = context.transform.localScale;
    }

    public override void Execute()
    {
        context.transform.localScale = context.transform.localScale - scale * Time.deltaTime * deltaBias;

        if (context.transform.localScale.x < 0)
        {
            context.transform.localScale = Vector3.zero;
            Managers.Resource.Destroy(context.gameObject);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

}