using System.Collections;
using UnityEngine;

public class SpreadBeaver : SkillController
{
    private Coroutine coroutine = null;

    private const float generateDelay = 0.1f;
    private const int rotationDegree = -25;

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
        Vector2 direction = Utils.GetRandomDirection(Vector2.up, 0, 30);
        for (int i = 0; i < CreateCount; ++i)
        {
            Quaternion rotation = Quaternion.AngleAxis(rotationDegree, Vector3.forward);
            direction = rotation * direction;

            GameObject bullet = Managers.Resource.Instantiate("Origin/Beaver", transform.position);
            bullet.GetOrAddComponent<BeaverController>().Direction = direction;
            Managers.Sound.Play("Beaver", Defines.Sound.Effect);

            yield return new WaitForSeconds(generateDelay);
        }

        Managers.Resource.Destroy(gameObject);
        yield break;
    }
}
