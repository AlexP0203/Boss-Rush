using UnityEditor;
using UnityEngine;

namespace AlexP
{
    [CustomEditor(typeof(Hearing))]

    public class HearingFOV : Editor
    {
        private void OnSceneGUI()
        {
            Hearing fov = (Hearing)target;
            Handles.color = Color.white;
            Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.getHearingRadius());

            Vector3 viewAngle01 = DirectionFromAngle(fov.transform.eulerAngles.y, -fov.getHearingAngle() / 2);
            Vector3 viewAngle02 = DirectionFromAngle(fov.transform.eulerAngles.y, fov.getHearingAngle() / 2);

            Handles.color = Color.yellow;
            Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle01 * fov.getHearingRadius());
            Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle02 * fov.getHearingRadius());
        }

        private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
        {
            angleInDegrees += eulerY;

            return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
        }
    }
}