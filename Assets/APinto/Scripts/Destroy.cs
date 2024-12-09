using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlexP
{
    public class Destroy : MonoBehaviour
    {

        [SerializeField] GameObject GameObject;
        public void destroy()
        {
            Destroy(GameObject);
        }
    }
}