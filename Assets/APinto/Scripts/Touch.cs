using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace AlexP
{
    public class Touch : MonoBehaviour
    {
        [SerializeField] GameObject boss;
        [SerializeField] GameObject playerRef;
        [SerializeField] LayerMask targetMask;
        [SerializeField] LayerMask obstructionMask;
        [SerializeField] float radius;
        [Range(0, 360)][SerializeField] float angle;

        bool canTouch;

        void Start()
        {
            playerRef = GameObject.FindGameObjectWithTag("Player");
            StartCoroutine(FOVRoutine());
        }

        void Update()
        {

            if (canTouch)
            {
                canTouch = false;
                boss.GetComponent<BossLogic>().DisableProjectileShield();
                enterMeleeState();
            }
            else
            {
                boss.GetComponent<BossLogic>().EnableProjectileShield();
            }
        }

        private void OnEnable()
        {
            StartCoroutine(FOVRoutine());
        }

        private IEnumerator FOVRoutine()
        {
            WaitForSeconds wait = new WaitForSeconds(0.2f);

            while (true)
            {
                yield return wait;
                FieldOfViewCheck();
            }
        }


        private void FieldOfViewCheck()
        {
            if (!canTouch)
            {
                Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

                if (rangeChecks.Length != 0)
                {
                    Transform target = rangeChecks[0].transform;
                    Vector3 directionToTarget = (target.position - transform.position).normalized;

                    if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
                    {
                        float distanceToTarget = Vector3.Distance(transform.position, target.position);

                        if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                        {
                            canTouch = true;
                        }
                        else
                        {
                            canTouch = false;
                        }
                    }
                    else
                    {
                        canTouch = false;
                    }
                }
                else if (canTouch)
                {
                    canTouch = false;
                }
            }

        }

        private void enterMeleeState()
        {
            boss.GetComponent<BossLogic>().MeleeState();
        }

        public float getTouchRadius()
        {
            return radius;
        }

        public float getTouchAngle()
        {
            return angle;
        }
    }
}