using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlexP
{
    public class Explosion : MonoBehaviour
    {

        private void Start()
        {
            StartCoroutine(Destroy());
        }

        IEnumerator Destroy()
        {
            yield return new WaitForSeconds(1.0f);
            Destroy(gameObject);
        }
    }
}