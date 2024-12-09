using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlexP
{
    public class animFireballAttack : StateMachineBehaviour
    {
        [SerializeField] GameObject Projectile;
        [SerializeField] float projectileSpeed;

        Transform Spawner;
        Transform Player;

        public void OnEnable()
        {
            Spawner = GameObject.Find("FireballSpawner").transform;
            Player = GameObject.Find("Player").transform;
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            GameObject fireball = Instantiate(Projectile, Spawner) as GameObject;
            Rigidbody rb = fireball.GetComponent<Rigidbody>();
            Vector3 direction = (Player.transform.position - Spawner.position).normalized;
            rb.AddForce(direction * projectileSpeed, ForceMode.Impulse);
        }
    }
}