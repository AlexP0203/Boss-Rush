using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlexP
{
    public class Fireball : MonoBehaviour
    {
        public GameObject projectile;
        public GameObject explosionVFX;

        [SerializeField] int damageAmount = 1;
        [SerializeField] float knockbackForce = 1;
        [SerializeField] GameObject hitEffectPrefab;
        [SerializeField] AudioClipCollection hitSounds;

        float fireballLife = 0;

        private void Start()
        {
            projectile.transform.parent = null;
        }

        private void Update()
        {
            fireballLife += 1 * Time.deltaTime;

            if( fireballLife > 7 )
            {
                destroyFireball();
            }
        }

        void OnCollisionEnter(Collision other)
        {
            Destroy(gameObject);
            Instantiate(explosionVFX, transform.position, transform.rotation);

            if (other.collider.GetComponent<Damageable>())
            {
                Vector3 dir = other.transform.position - transform.position;
                dir.Normalize();

                Damage damage = new Damage();
                damage.amount = damageAmount;
                damage.direction = dir;
                damage.knockbackForce = knockbackForce;

                if (other.collider.GetComponent<Damageable>().Hit(damage))
                {
                    if (hitEffectPrefab != null)
                    {
                        Instantiate(hitEffectPrefab, other.transform.position, Quaternion.identity);
                    }

                    if (hitSounds != null)
                        SoundEffectsManager.instance.PlayRandomClip(hitSounds.clips, true);
                }
            }
        }

        private void destroyFireball()
        {
            Destroy(gameObject);
        }
    }
}