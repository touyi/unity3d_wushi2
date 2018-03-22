using UnityEngine;
using System.Collections;

public class Agent : MonoBehaviour
{

    public BlackBoard BlackBoard;
    protected FSMManager _fsm;

    public virtual void Move(Vector3 pos)
    {

    }
}
