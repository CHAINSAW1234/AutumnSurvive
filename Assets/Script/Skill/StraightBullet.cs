using System.Collections;
using UnityEngine;

public class StraightBullet : SkillController
{
    private int createCount = 15;
    private Coroutine coroutine = null;

    private const float GenerateDelay = 0.1f;
    private const int RotationDegree = 25;
    protected override void Awake()
    {
        base.Awake();
        skill = Defines.Skill.StraightBullet;

        stateMachine.RegisterState<StateSkillFollow>(Defines.State.Follow, this);
        Direction = new Vector2(0f, 1f);
        createCount = 30;
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
        for (int i = 0; i < createCount; ++i)
        {
            if(Managers.Input.TouchDirection != Vector3.zero)
            {
                direction = Managers.Input.TouchDirection.normalized;
            }

            GameObject bullet = Managers.Resource.Instantiate("Bullet", transform.position);
            BulletController bulletController = bullet.GetOrAddComponent<BulletController>();
            bulletController.Direction = direction;
            bulletController.MoveSpeed = Mathf.Max(0.1f, Managers.Input.TouchDirectionMagnitude) * bulletController.MoveSpeed;

            yield return new WaitForSeconds(GenerateDelay);
        }

        Managers.Resource.Destroy(gameObject);
        yield break;
    }
}
