using System.Collections;
using UnityEngine;

public class StraightBeaver : SkillController
{
    private Coroutine coroutine = null;

    private const float minimumSpeed = 0.1f;
    private const float generateDelay = 0.1f;

    protected override void Awake()
    {
        skill = Defines.Skill.StraightBeaver;
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
        Vector2 direction = Vector2.up;
        for (int i = 0; i < CreateCount; ++i)
        {
            if(Managers.Input.TouchDirection != Vector3.zero)
            {
                direction = Managers.Input.TouchDirection.normalized;
            }

            GameObject bullet = Managers.Resource.Instantiate("Origin/Beaver", transform.position);
            BeaverController bulletController = bullet.GetOrAddComponent<BeaverController>();
            bulletController.Direction = direction;
            bulletController.MoveSpeed = Mathf.Max(minimumSpeed, Managers.Input.TouchDirectionMagnitude) * bulletController.MoveSpeed;
            Managers.Sound.Play("Beaver", Defines.Sound.Effect);

            yield return new WaitForSeconds(generateDelay);
        }

        Managers.Resource.Destroy(gameObject);
        yield break;
    }
}
