using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

namespace AlexP
{
    public class Spawner : MonoBehaviour
    {
        public GameObject Projectile;
        public float projectileSpeed;
        public GameObject player;
        public float timeBetweenFireballs;
        public float timeBetweenAOEAttack;

        bool enableFireball;
        

        void Update()
        {
            timeBetweenFireballs += 1 * Time.deltaTime;
            timeBetweenAOEAttack += 1 * Time.deltaTime;

            if (timeBetweenFireballs > 1 && enableFireball == false)
            {
                Fireball();
                timeBetweenFireballs = 0;
            }

            if (timeBetweenAOEAttack > 20)
            {
                enableFireball = true;
                Fireball();
                StartCoroutine(AOEAttack());
            }
        }

        private void Fireball()
        {
            GameObject fireball = Instantiate(Projectile, transform) as GameObject;
            Rigidbody rb = fireball.GetComponent<Rigidbody>();
            Vector3 direction = (player.transform.position - transform.position).normalized;
            rb.AddForce(direction * projectileSpeed, ForceMode.Impulse);
        }

        IEnumerator AOEAttack()
        {
            yield return new WaitForSeconds(10.0f);
            timeBetweenAOEAttack = 0;
            enableFireball = false;
        }
    }
}