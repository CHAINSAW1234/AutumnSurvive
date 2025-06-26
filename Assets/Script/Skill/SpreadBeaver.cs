using System.Collections;
using UnityEngine;

public class SpreadBeaver : SkillController
{
    private Coroutine coroutine = null;

    private const float GenerateDelay = 0.1f;
    private const int RotationDegree = -25;
    protected override void Awake()
    {
        skill = Defines.Skill.SpreadBeaver;
        base.Awake();

        stateMachine.RegisterState<StateSkillFollow>(Defines.State.Follow, this);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        State = Defines.State.Follow;
        coroutine = StartCoroutine(GenerateBeaver());
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        StopCoroutine(coroutine);
        coroutine = null;
    }
    private IEnumerator GenerateBeaver()
    {
        Vector2 direction = Utils.GetRandomDirection(new Vector2(0, 1f), 0, 180);
        for (int i = 0; i < CreateCount; ++i)
        {
            Quaternion rotation = Quaternion.AngleAxis(RotationDegree, Vector3.forward);
            direction = rotation * direction;

            GameObject bullet = Managers.Resource.Instantiate("Origin/Bullet", transform.position);
            bullet.GetOrAddComponent<BulletController>().Direction = direction;

            yield return new WaitForSeconds(GenerateDelay);
        }

        Managers.Resource.Destroy(gameObject);
        yield break;
    }
}
