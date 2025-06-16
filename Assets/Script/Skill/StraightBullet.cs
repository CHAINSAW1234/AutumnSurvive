using System.Collections;
using UnityEngine;

public class StraightBullet : SkillController
{
    private Coroutine coroutine = null;

    private const float MagnitudeBias = 0.1f;
    private const float GenerateDelay = 0.1f;

    protected override void Awake()
    {
        skill = Defines.Skill.StraightBullet;
        base.Awake();

        stateMachine.RegisterState<StateSkillFollow>(Defines.State.Follow, this);
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
        Vector2 direction = new Vector2(0, 1f);
        for (int i = 0; i < CreateCount; ++i)
        {
            if(Managers.Input.TouchDirection != Vector3.zero)
            {
                direction = Managers.Input.TouchDirection.normalized;
            }

            GameObject bullet = Managers.Resource.Instantiate("Bullet", transform.position);
            BulletController bulletController = bullet.GetOrAddComponent<BulletController>();
            bulletController.Direction = direction;
            bulletController.MoveSpeed = Mathf.Max(0.1f, Managers.Input.TouchDirectionMagnitude) * bulletController.MoveSpeed + MagnitudeBias;

            yield return new WaitForSeconds(GenerateDelay);
        }

        Managers.Resource.Destroy(gameObject);
        yield break;
    }
}
