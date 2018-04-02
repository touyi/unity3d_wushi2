using UnityEngine;
using System.Collections;

[System.Serializable]
public class BlackBoard
{
    public Animator anim;
    public Quaternion finRotation;

    public float speedSmooth = 10f;
    public float rotaSpeed = 10f;
    public float maxRunSpeed = 5f;
    public float nowSpeed = 0f;
    public float beginSpeed = 2.5f;
    public float SpeedModifiler = 1f;
}
