using System.Collections;
using UnityEngine;

namespace AlexP
{
    public class Sight : MonoBehaviour
    {
        [SerializeField] GameObject boss;
        [SerializeField] GameObject playerRef;
        [SerializeField] LayerMask targetMask;
        [SerializeField] LayerMask obstructionMask;
        [SerializeField] float radius;
        [Range(0, 360)][SerializeField] float angle;

        bool canSee;

        void Start()
        {
            
            playerRef = GameObject.FindGameObjectWithTag("Player");
            StartCoroutine(FOVRoutine());
        }

        void Update()
        {
            if (canSee)
            {
            }
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
            if (!canSee)
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
                            canSee = true;
                        }
                        else
                        {
                            canSee = false;
                        }
                    }
                    else
                    {
                        canSee = false;
                    }
                }
                else if (canSee)
                {
                    canSee = false;
                }
            }

        }

        public float getSightRadius()
        {
            return radius;
        }

        public float getSightAngle()
        {
            return angle;
        }
    }
}
