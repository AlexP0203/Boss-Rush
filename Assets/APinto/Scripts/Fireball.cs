using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlexP
{
    public class Fireball : MonoBehaviour
    {
        [SerializeField] GameObject projectile;
        [SerializeField] GameObject explosionVFX;

        [SerializeField] int damageAmount = 1;
        [SerializeField] float knockbackForce = 1;
        [SerializeField] GameObject hitEffectPrefab;
        [SerializeField] AudioClipCollection hitSounds;
        [SerializeField] AudioClipCollection fireballSound;

        float fireballLife = 0;

        private void Start()
        {
            projectile.transform.parent = null;
            SoundEffectsManager.instance.PlayRandomClip(fireballSound.clips, true);
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