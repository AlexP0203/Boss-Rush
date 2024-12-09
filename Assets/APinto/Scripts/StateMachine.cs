using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using brolive;

namespace AlexP
{
    public class StateMachine
    {
        public State currentState;
        public BossLogic myBoss;

        public StateMachine(BossLogic boss)
        {
            myBoss = boss;
        }

        public void Update()
        {
            currentState.OnUpdate();
        }

        public void ChangeState(State newState)
        {
            if (currentState != null)
            {
                currentState.OnExit();
            }

            currentState = newState;

            newState.OnEnter();
        }
    }
}
