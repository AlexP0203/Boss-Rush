using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AlexP
{
    public class ForceFieldLogic : MonoBehaviour
    {
        public Material Material1;
        public GameObject forceField;
        public GameObject dragon;

        [Range(0f, 1f)] public float distance;

        private void Start()
        {
            forceField.transform.parent = null;
        }
        private void LateUpdate()
        {
            transform.position = Vector3.MoveTowards(transform.position, dragon.transform.position, distance);
        }
    }
}