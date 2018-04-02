using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public CameraOffset target;
    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.CameraPos, Time.deltaTime * 4);
        Quaternion lookat = Quaternion.LookRotation(target.transform.position - transform.position);

        transform.rotation = Quaternion.Lerp(transform.rotation, lookat, Time.deltaTime * 4);
    }
}
