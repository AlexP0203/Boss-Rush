using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace AlexP
{
    public class BossLogic : MonoBehaviour
    {
        [SerializeField] Transform target;
        [SerializeField] GameObject boss;
        [SerializeField] GameObject forceField;
        [SerializeField] GameObject projectileShield;
        [SerializeField] GameObject thunder;
        [SerializeField] GameObject flame;
        [SerializeField] GameObject fireballSpawner;
        [SerializeField] GameObject basicAttackObject;

        [SerializeField] Animator animator;
        [SerializeField] GameObject ears;
        [SerializeField] GameObject eyes;
        [SerializeField] GameObject touch;

        StateMachine myStateMachine;

        bool pushBackAttackInitiated;
        bool toRangedState;
        bool withinTouch;
        int closeRangehits;
        int damageDone;
        int shieldDamage;

        //****************************  Start/Update  *****************************
        #region Start/Update
        void Start()
        {
            myStateMachine = new StateMachine(this);
            IdleState();
        }


        void Update()
        {
            myStateMachine.Update();

            if (closeRangehits >= 3)
            {
                StartLongRangeThunder();
            }

            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f)
            {
                IdleAnimation();
            }

            if (damageDone == 10)
            {
                Destroy(gameObject);
            }

            if(shieldDamage == 9)
            {
                forceField.SetActive(false);
                shieldDamage = 0;
            }
        }
        #endregion
        //*************************************************************************


        //**************************** Change States ******************************
        #region Change States
        public void IdleState()
        {
            myStateMachine.ChangeState(new IdleState(myStateMachine));
        }

        public void RangedState()
        {
            myStateMachine.ChangeState(new RangedState(myStateMachine));
        }

        public void PursueState()
        {
            myStateMachine.ChangeState(new PursueState(myStateMachine));
        }

        public void MeleeState()
        {
            touch.SetActive(false);
            myStateMachine.ChangeState(new MeleeState(myStateMachine));
        }

        //-------------------------------------------------------------------------
        public void ForceFieldRangedState()
        {
            if (toRangedState == false)
            {
                toRangedState = true;
                RangedState();
            }
        }

        IEnumerator ReturnRangedState()
        {
            yield return new WaitForSeconds(5.0f);
            thunder.SetActive(false);
            touch.SetActive(true);
            EnableDragonAllColliders();
            RangedState();
        }
        #endregion
        //*************************************************************************


        //************************** Abilities/Attacks ****************************
        #region Abilities/Attacks
        public void RangedAttack()
        {
            FireballAnimation();
            StartCoroutine(BackToIdleTimer());
        }

        public void FlameAttack()
        {
            flame.SetActive(true);
            StartCoroutine(disableFlame());
        }

        public void BasicAttack()
        {
            BasicAttackAnimation();
        }

        public void EnableBasicAttackObject()
        {
            StartCoroutine(basicAttackTimer());
        }

        public void DisableBasicAttackObject()
        {
            basicAttackObject.SetActive(false);
        }

        public void ForceField()
        {
            forceField.SetActive(true);
        }

        public void ThunderAttack()
        {
            thunder.SetActive(true);
        }

        public void StartLongRangeThunder()
        {
            DisableDragonAllColliders();
            StartCoroutine(EnableDragonAllCollidersTimer());
            StartCoroutine(EnableThunder());
            closeRangehits = 0;
        }

        //-------------------------------------------------------------------------
        IEnumerator BackToIdleTimer()
        {
            yield return new WaitForSeconds(1.0f);
            IdleAnimation();
        }

        IEnumerator EnableThunder()
        {
            IdleAnimation();
            yield return new WaitForSeconds(0.5f);

            ThunderAnimation();
            StartCoroutine(ReturnRangedState());
        }

        IEnumerator basicAttackTimer()
        {
            yield return new WaitForSeconds(0.5f);
            basicAttackObject.SetActive(true);
        }

        public void LoadUp()
        {
            StartCoroutine(enableThunderAndForceField());
        }

        IEnumerator enableThunderAndForceField()
        {
            yield return new WaitForSeconds(2.0f);

            ThunderAttack();
            ForceField();
        }

        IEnumerator disableFlame()
        {
            yield return new WaitForSeconds(4.0f);

            if (flame != null)
            {
                flame.GetComponent<Destroy>().destroy();
            }
            //damageDone = 3;
        }
        #endregion
        //*************************************************************************


        //****************************** Animations *******************************
        #region Animations
        public Animator GetAnimator()
        {
            return animator;
        }

        public void IdleAnimation()
        {
            animator.SetTrigger("Idle");
        }

        public void FireballAnimation()
        {
            animator.SetTrigger("Fireball");
        }

        public void FlameAnimation()
        {
            animator.SetTrigger("Flame Attack");
        }

        public void BasicAttackAnimation()
        {
            animator.SetTrigger("Basic Attack");
        }

        public void ThunderAnimation()
        {
            animator.SetTrigger("Scream");
        }

        public void HitAnimation()
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Get Hit"))
            {
                animator.SetTrigger("Hit");
            }
            else
            {
                IdleAnimation();
            }
        }
        #endregion
        //*************************************************************************


        //************************* Children/Components ***************************
        #region Children/Components
        public void EnableFireballSpawner()
        {
            fireballSpawner.SetActive(true);
        }


        public void DisableFireballSpawner()
        {
            fireballSpawner.SetActive(false);
        }

        public void DisableProjectileShield()
        {
            projectileShield.SetActive(false);
        }

        public void EnableProjectileShield()
        {
            projectileShield.SetActive(true);
        }


        public void DisableDragonAllColliders()
        {
            foreach (Transform child in transform)
            {
                if (child.name == "Dragon")
                {
                    foreach (Collider c in GetComponentsInChildren<Collider>())
                    {
                        c.enabled = false;
                    }
                }
            }
        }

        public void EnableDragonAllColliders()
        {
            foreach (Transform child in transform)
            {
                if (child.name == "Dragon")
                {
                    foreach (Collider c in GetComponentsInChildren<Collider>())
                    {
                        c.enabled = true;
                    }
                }
            }
        }
        //-------------------------------------------------------------------------
        public IEnumerator EnableDragonAllCollidersTimer()
        {
            yield return new WaitForSeconds(5.5f);
            EnableDragonAllColliders();
        }
        #endregion
        //*************************************************************************


        //*************************** Other Functions *****************************
        #region Other Functions
        public void FacePlayer()
        {
            if (!pushBackAttackInitiated)
            {
                if (target != null)
                {
                    transform.LookAt(target);
                }
            }
        }

        public void ChangeToRangedState()
        {
            toRangedState = false;
        }

        public void WithinTouch()
        {
            withinTouch = !withinTouch;
        }

        public void PushBackAttackInitiated()
        {
            pushBackAttackInitiated = !pushBackAttackInitiated;
        }
        public void SetDamageDone(int x)
        {
            damageDone = 7;
        }

        public void ShieldDamage()
        {
            shieldDamage++;
        }

        public int GetDamageDone()
        {
            return damageDone;
        }

        public void IncreaseDamageDone()
        {
            damageDone++;
        }

        public void IncreasecloseRangehits()
        {
            if (withinTouch == true)
            {
                closeRangehits++;
            }
        }
        #endregion
        //*************************************************************************
    }
}


