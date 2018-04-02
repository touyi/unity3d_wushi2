using UnityEngine;
using System.Collections;

public class CameraTrigger : MonoBehaviour {
    public Transform cameraOffset;
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, 0.3f);
        if (cameraOffset != null)
        {
            Gizmos.DrawSphere(cameraOffset.position, 0.3f);
            Gizmos.DrawLine(transform.position, cameraOffset.position);
        }
    }
}
