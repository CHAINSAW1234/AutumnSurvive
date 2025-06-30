using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squirrals : SkillController
{
    private const float generateBias = 0.05f;
   
    protected override void Awake()
    {
        skill = Defines.Skill.Squirrals;
        base.Awake();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        StartCoroutine(CreateSquirral());

    }
    
    IEnumerator CreateSquirral()
    {
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Enemy");

        List<GameObject> visibleMonsters = new List<GameObject>();

        foreach (GameObject monster in monsters)
        {
            SpriteRenderer renderer = monster.GetComponentInChildren<SpriteRenderer>();
            if (renderer != null && renderer.isVisible)
            {
                visibleMonsters.Add(monster);
            }
        }

        List<GameObject> randomMosters = Utils.GetRandomUnique(visibleMonsters.ToArray(), CreateCount);

        for (int i = 0; i < randomMosters.Count; ++i)
        {
            GameObject obj = Managers.Resource.Instantiate("Squirral", transform.position);
            obj.GetOrAddComponent<Squirral>().SetTarget(randomMosters[i].transform);
            Managers.Sound.Play("Squirral", Defines.Sound.Effect);
            yield return new WaitForSeconds(generateBias);
        }

        Managers.Resource.Destroy(gameObject);
        yield break;
    }

    public class Squirral : SkillController
    {
        private Vector3 scale;

        private const int scaleBias = 3;
        protected override void Awake()
        {
            skill = Defines.Skill.Squirrals;
            base.Awake();

            scale = transform.localScale;
            stateMachine.RegisterState<StateSkillChase>(Defines.State.Chase, this);
            transform.localScale = transform.localScale * scaleBias;
            stateMachine.RegisterState<StateSkillAppear>(Defines.State.Appear, this);
            stateMachine.RegisterState<StateSkillWait>(Defines.State.Wait, this);
            stateMachine.RegisterState<StateSkillDisappear>(Defines.State.Disappear, this);
            transform.localScale = transform.localScale / scaleBias;
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
