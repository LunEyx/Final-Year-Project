using System.Collections;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SightOfView))]
public class EditorSightOfView: Editor
{
    private void OnSceneGUI()
    {
        SightOfView sov = (SightOfView)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(sov.transform.position, Vector3.up, Vector3.forward, 360, sov.viewRadius);
        Vector3 viewAngleA = sov.DirFromAngle(-sov.viewAngle / 2, false);
        Vector3 viewAngleB = sov.DirFromAngle(sov.viewAngle / 2, false);

        Handles.DrawLine(sov.transform.position, sov.transform.position + viewAngleA * sov.viewRadius);
        Handles.DrawLine(sov.transform.position, sov.transform.position + viewAngleB * sov.viewRadius);

        Handles.color = Color.red;
        foreach(Transform visibleTarget in sov.visibleTargets)
        {
            Handles.DrawLine(sov.transform.position, visibleTarget.position);
        }
    }
}
