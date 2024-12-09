using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlexP
{
    public class DisableControls : MonoBehaviour
    {
        public void DisablePlayerControls()
        {
            StaticInputManager.input.Disable();
        }

        public void EnablePlayerControls()
        {
            StaticInputManager.input.Enable();
        }
    }
}