using System.Collections;
using UnityEngine;

namespace AlexP
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] GameObject Projectile;
        [SerializeField] GameObject player;
        [SerializeField] float projectileSpeed;
        [SerializeField] AudioClip fireballSound;
        [SerializeField] AudioClip flameSound;

        public void Fireball()
        {
            GameObject fireball = Instantiate(Projectile, transform) as GameObject;
            Rigidbody rb = fireball.GetComponent<Rigidbody>();
            Vector3 direction = (player.transform.position - transform.position).normalized;
            rb.AddForce(direction * projectileSpeed, ForceMode.Impulse);
            SoundEffectsManager.instance.PlayAudioClip(fireballSound, true);
        }

        public void flameTimer()
        {
            StartCoroutine(Flame());
        }

        IEnumerator Flame()
        {
            yield return new WaitForSeconds(0.01f);
            GameObject fireball = Instantiate(Projectile, transform) as GameObject;
            Rigidbody rb = fireball.GetComponent<Rigidbody>();
            Vector3 direction = (player.transform.position - transform.position).normalized;
            rb.AddForce(direction * projectileSpeed, ForceMode.Impulse);
            flameSoundStart();
        }

        public void flameSoundStart()
        {
            if(!SoundEffectsManager.instance)
            {
                SoundEffectsManager.instance.PlayAudioClip(flameSound, true);
            }
        }
    }
}