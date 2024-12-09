using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlexP
{
    public class animBasicAttack : StateMachineBehaviour
    {
        GameObject Dragon;

        private void OnEnable()
        {
            Dragon = GameObject.Find("Blue");
        }

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Dragon.GetComponent<BossLogic>().EnableBasicAttackObject();
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Dragon.GetComponent<BossLogic>().DisableBasicAttackObject();
        }
    }
}