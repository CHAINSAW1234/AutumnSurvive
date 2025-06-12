using System;
using System.Collections.Generic;
using UnityEngine;

public class Squirrals : SkillController
{
    private int createCount = 6;
    protected override void Awake()
    {
        base.Awake();
        skill = Defines.Skill.Squirral;

    }

    protected override void OnEnable()
    {
        base.OnEnable();
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Enemy");
        List<GameObject> randomMosters = Utils.GetRandomUnique(monsters, createCount);

        for (int i = 0; i < createCount; ++i)
        {
            if (randomMosters.Count == 0)
                break;

            GameObject obj = Managers.Resource.Instantiate("Squirral", transform.position);
            obj.GetOrAddComponent<Squirral>().SetTarget(randomMosters[i % randomMosters.Count].transform);
        }

        Managers.Resource.Destroy(gameObject);
    }

    public class Squirral : SkillController
    {
        private Vector3 scale;
        protected override void Awake()
        {
            base.Awake();
            skill = Defines.Skill.Squirral;
            scale = transform.localScale;
            stateMachine.RegisterState<StateSkillChase>(Defines.State.Chase, this);
            transform.localScale = transform.localScale * 3;
            stateMachine.RegisterState<StateSkillAppear>(Defines.State.Appear, this);
            stateMachine.RegisterState<StateSkillWait>(Defines.State.Wait, this);
            stateMachine.RegisterState<StateSkillDisappear>(Defines.State.Disappear, this);
            transform.localScale = transform.localScale / 3;

            Duration = 22;
            MoveSpeed = 5f;
        }
        protected override void OnEnable()
        {
            base.OnEnable();
            State = Defines.State.Chase;
            transform.localScale = scale;
        }

        public void SetTarget(Transform transform)
        {
            StateSkillChase state = stateMachine.GetState<StateSkillChase>(Defines.State.Chase);

            if(state != null)
            {
                state.TargetTransform = transform;
            }
        }
    }

}
