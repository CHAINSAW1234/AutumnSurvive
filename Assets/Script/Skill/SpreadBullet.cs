using System.Collections;
using UnityEngine;

public class SpreadBullet : SkillController
{
    private Coroutine coroutine = null;

    private const float GenerateDelay = 0.1f;
    private const int RotationDegree = -25;
    protected override void Awake()
    {
        base.Awake();
        skill = Defines.Skill.SpreadBullet;

        stateMachine.RegisterState<StateSkillFollow>(Defines.State.Follow, this);
        Direction = new Vector2(0f, 1f);
        CreateCount = 30;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        State = Defines.State.Follow;
        coroutine = StartCoroutine(GenerateBullet());
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        StopCoroutine(coroutine);
        coroutine = null;
    }
    private IEnumerator GenerateBullet()
    {
        Vector2 direction = Utils.GetRandomDirection(new Vector2(0, 1f), 0, 180);
        for (int i = 0; i < CreateCount; ++i)
        {
            Quaternion rotation = Quaternion.AngleAxis(RotationDegree, Vector3.forward);
            direction = rotation * direction;

            GameObject bullet = Managers.Resource.Instantiate("Bullet", transform.position);
            bullet.GetOrAddComponent<BulletController>().Direction = direction;

            yield return new WaitForSeconds(GenerateDelay);
        }

        Managers.Resource.Destroy(gameObject);
        yield break;
    }
}
