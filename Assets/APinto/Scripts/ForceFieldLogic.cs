using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AlexP
{
    public class ForceFieldLogic : MonoBehaviour
    {
        [SerializeField] Material Material1;
        [SerializeField] GameObject forceField;
        [SerializeField] GameObject dragon;

        [Range(0f, 1f)] public float distance;

        private void OnEnable()
        {
            gameObject.GetComponent<Renderer>().material = Material1;
        }

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