using UnityEngine;
using System.Collections;

public class Player : Agent
{
    CharacterController controller = null;

    
    public void Awake()
    {
        _fsm = new FSMManager(this, E_ActionTYpe.Idle);
        _fsm.Addaction(FSMActionFactory.Create(E_ActionTYpe.Idle, _fsm));
        _fsm.Addaction(FSMActionFactory.Create(E_ActionTYpe.Walk, _fsm));
        _fsm.Initialize();

        controller = GetComponent<CharacterController>();
    }

    private void Start()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, Vector3.down,out hit, 50f))
        {
            transform.position = hit.point;
        }
    }
    public override void Move(Vector3 pos)
    {
        controller.Move(pos * Time.deltaTime * BlackBoard.nowSpeed);
    }

    private void Update()
    {
        _fsm.Update();
    }
}
