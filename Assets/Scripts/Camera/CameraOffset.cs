using UnityEngine;
using System.Collections;

public class CameraOffset : MonoBehaviour {

    GameObject offsetObject;
    Transform offset;
    public Vector3 defaultOffset;
    float time = 2f;
    private void Awake()
    {
        offsetObject = new GameObject("offsetObject");
        offset = offsetObject.transform;
    }
    public Vector3 CameraPos
    {
        get
        {
            return offset.position + transform.position;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        CameraTrigger trigger = other.GetComponent<CameraTrigger>();
        if (trigger == null) return;
        if (trigger.cameraOffset == null)
        {
            iTween.MoveTo(offsetObject, defaultOffset, time);
        }
        else
        {
            Vector3 offsetPos = trigger.cameraOffset.localPosition;
            iTween.MoveTo(offsetObject, offsetPos, time);
        }
    }
}
