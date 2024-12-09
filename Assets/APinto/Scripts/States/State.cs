using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlexP
{
    public class State
    {
        public StateMachine machine;
        public float timeBetweenFireballs;
        public float timeBetweenMeleeAttack;

        public State(StateMachine m)
        {
            machine = m;
        }

        public virtual void OnEnter()
        {

        }

        public virtual void OnUpdate()
        {

        }

        public virtual void OnExit()
        {

        }
    }
}
