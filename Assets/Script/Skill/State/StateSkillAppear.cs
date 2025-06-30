using UnityEngine;

    public class StateSkillAppear : State
    {
        SkillController context = null;
        private Vector3 scale = Vector3.zero;

        private const float deltaBias = 1.5f;
        public StateSkillAppear(StateMachine stateMachine, SkillController context) : base(stateMachine)
        {
            this.context = context;
            scale = context.transform.localScale;
        }

        public override void Enter()
        {
            context.transform.localScale = Vector3.zero;
        }
        public override void Execute()
        {
            context.transform.localScale = context.transform.localScale + scale * Time.deltaTime * deltaBias;

            if(context.transform.localScale.x > scale.x)
            {
                context.transform.localScale = scale;
                context.State = Defines.State.Wait;
            }
        }
    }