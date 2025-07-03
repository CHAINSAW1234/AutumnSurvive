
using UnityEngine;


public class StateSkillChase : State
{
    SkillController context = null;

    public Transform TargetTransform { get; set; } = null;
    private Vector3 targetPosition = Vector3.zero;
    private Vector3 startPosition = Vector3.zero;
    private GameObject reticle = null;

    private float progress = 0f;

    private const float moveDuration = 1f;   // 이동 시간
    private const float maxRadius = 3f;      // 나선 반지름 (최대 값)

    public StateSkillChase(StateMachine stateMachine, SkillController context) : base(stateMachine)
    {
        this.context = context;
    }

    public override void Enter()
    {
        base.Enter();
        startPosition = context.transform.position;
        reticle = Managers.Resource.Instantiate("Reticle");
        progress = 0;
    }
    public override void Execute()
    {
        if(TargetTransform != null && TargetTransform.gameObject.activeInHierarchy)
        {
            targetPosition = TargetTransform.position;
        }
        else
        {
            TargetTransform = null;
        }

        reticle.transform.position = targetPosition;

        Chase();
    }

    public override void Exit()
    {
        Managers.Resource.Destroy(reticle);
    }

    private void Chase()
    {
        progress += Time.deltaTime;

        Vector3 centerPosition = Vector3.Lerp(startPosition, targetPosition, progress);

        Vector3 direction = (targetPosition - startPosition).normalized;
        Vector3 perpendicular = new Vector3(-direction.y, direction.x, 0f); 

        float angle = Mathf.Lerp(0, Mathf.PI, progress);
        float radius = Mathf.Lerp(maxRadius, 0, progress);

        Vector3 offset = perpendicular * Mathf.Sin(angle) * radius;

        context.transform.position = centerPosition + offset;

        if (progress >= moveDuration)
        {
            context.transform.position = targetPosition;
            context.State = Defines.State.Appear;
        }
    }
}