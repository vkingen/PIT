using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FieldOfView))]
public class FieldOfViewEditor : Editor
{
    private void OnSceneGUI()
    {
        FieldOfView fov = (FieldOfView)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.viewRadius);
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.attackRadius);
        Vector3 viewAngleA = fov.DirectionFromAngle(-fov.viewAngle / 2, false);
        Vector3 viewAngleB = fov.DirectionFromAngle(fov.viewAngle / 2, false);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleA * fov.viewRadius);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleB * fov.viewRadius);

        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleA * fov.attackRadius);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleB * fov.attackRadius);



        Handles.color = Color.red;
        if (fov.visibleTarget != null)
        {
            Handles.DrawLine(fov.headLocation.position, fov.visibleTarget.position);
        }

        Handles.color = Color.green;
        if (fov.attackTarget != null)
        {
            Handles.DrawLine(fov.headLocation.position, fov.attackTarget.position);
        }

    }
}
