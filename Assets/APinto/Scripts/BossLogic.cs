using brolive;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace AlexP
{
    public class LookAtTarget : MonoBehaviour
    {
        [SerializeField] Transform target;
        [SerializeField] GameObject forceField;



        void Start()
        {
            Instantiate(forceField, transform);
        }
        void Update()
        {
            if (target != null)
            {
                transform.LookAt(target);
            }

        }

        public void Death()
        {
            Destroy(gameObject);
        }
    }
}


