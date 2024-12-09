using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlexP
{
    public class animThunderAttack : StateMachineBehaviour
    {

        [SerializeField] AudioClipCollection scream;
        GameObject Dragon;

        private void OnEnable()
        {
            Dragon = GameObject.Find("Blue");
        }

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            SoundEffectsManager.instance.PlayRandomClip(scream.clips, true);
            Dragon.GetComponent<BossLogic>().LoadUp();
            Dragon.GetComponent<BossLogic>().PushBackAttackInitiated();
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Dragon.GetComponent<BossLogic>().PushBackAttackInitiated();
        }
    }
}