using UnityEngine;

namespace AlexP
{
    public class RangedState : State
    {
        public RangedState(StateMachine m) : base(m)
        {
            machine = m;
        }
        public override void OnEnter()
        {
            base.OnEnter();
            machine.myBoss.IdleAnimation();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            machine.myBoss.FacePlayer();

            timeBetweenFireballs += 1 * Time.deltaTime;

            if (timeBetweenFireballs > 2.0f && machine.myBoss.GetDamageDone() < 3)
            {
                machine.myBoss.RangedAttack();
                timeBetweenFireballs = 0.0f;
            }

            if (machine.myBoss.GetDamageDone() >= 3 && machine.myBoss.GetDamageDone() < 6)
            {
                machine.myBoss.RangedAttack();
            }

            if (machine.myBoss.GetDamageDone() == 6)
            {
                machine.myBoss.FlameAnimation();
            }

            if (machine.myBoss.GetDamageDone() > 6 && machine.myBoss.GetDamageDone() < 10)
            {
                machine.myBoss.RangedAttack();
            }
        }

        public override void OnExit()
        {
            timeBetweenFireballs = 0;
            base.OnExit();
        }
    }
}