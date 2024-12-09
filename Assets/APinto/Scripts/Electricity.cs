using JetBrains.Annotations;
using UnityEngine;

namespace AlexP
{
    public class Electricity : MonoBehaviour
    {

        [SerializeField] int damageAmount = 1;
        [SerializeField] float knockbackForce = 1;
        [SerializeField] GameObject hitEffectPrefab;
        [SerializeField] AudioClipCollection hitSounds;
        [SerializeField] GameObject dragon;

        void OnTriggerEnter(Collider other)
        {

            if (other.GetComponent<Damageable>())
            {
                Vector3 dir = other.transform.position - transform.position;
                dir.Normalize();

                Damage damage = new Damage();
                damage.amount = damageAmount;
                damage.direction = dir;
                damage.knockbackForce = knockbackForce;

                if (other.GetComponent<Damageable>().Hit(damage))
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
    }
}

