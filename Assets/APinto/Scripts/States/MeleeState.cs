using UnityEngine;

namespace AlexP
{
    public class MeleeState : State
    {
        public MeleeState(StateMachine m) : base(m)
        {
            machine = m;
        }
        public override void OnEnter()
        {
            base.OnEnter();
            machine.myBoss.DisableFireballSpawner();
            machine.myBoss.GetComponent<BossLogic>().WithinTouch();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            machine.myBoss.FacePlayer();

            timeBetweenMeleeAttack += 1 * Time.deltaTime;

            if (timeBetweenMeleeAttack > 3.0f)
            {
                machine.myBoss.BasicAttack();

                if (machine.myBoss.GetDamageDone() < 6)
                {
                    timeBetweenMeleeAttack = 0.0f;
                }
                if (machine.myBoss.GetDamageDone() >= 6 && machine.myBoss.GetDamageDone() < 12)
                {
                    timeBetweenMeleeAttack = 1.0f;
                }
                if (machine.myBoss.GetDamageDone() >= 12)
                {
                    timeBetweenMeleeAttack = 2.0f;
                }
            }
        }

        public override void OnExit()
        {
            machine.myBoss.EnableFireballSpawner();
            machine.myBoss.GetComponent<BossLogic>().WithinTouch();
            timeBetweenMeleeAttack = 0;
            base.OnExit();
        }
    }
}
