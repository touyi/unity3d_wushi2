using UnityEngine;
using System.Collections;

public class PlayerWalkAction : FSMBaseAction
{
    BlackBoard _blackBoard;
    public PlayerWalkAction(Agent agent, FSMManager owner) : base(E_ActionTYpe.Walk, agent, owner)
    {
    }
    public override void Initialize()
    {
        _blackBoard = _agent.BlackBoard;
    }
    public override void BuildRelation()
    {
        FSMWorldState state = FSMWroldStateFactory.Creat();
        state.SetProperty(E_PropType.Speed, 1);
        _owner.AddRelation(E_ActionTYpe.Idle, E_ActionTYpe.Walk, state, 1);
    }
    public override void Active()
    {
        _blackBoard.nowSpeed = _blackBoard.beginSpeed;
    }
    public override void Listen()
    {
        if (InputManager.GetInputVector2().magnitude>=0.2)
        {
            _owner.SetProperty(E_PropType.Speed, 1);
        }
    }

    public override void Update()
    {
        Vector2 move = InputManager.GetInputVector2();
        Vector3 moveDir = new Vector3(move.x, 0f, move.y).normalized;
        
        _blackBoard.finRotation.SetLookRotation(moveDir);


        float angle = Quaternion.Angle(_agent.transform.rotation, _blackBoard.finRotation);
        _agent.transform.rotation = Quaternion.Slerp(_agent.transform.rotation, _blackBoard.finRotation, /*angle */ Time.deltaTime * _agent.BlackBoard.rotaSpeed);
        angle = Quaternion.Angle(_agent.transform.rotation, _blackBoard.finRotation);
        if(angle < 45f)
        {
            _blackBoard.nowSpeed = Mathf.Lerp(_blackBoard.nowSpeed, _blackBoard.maxRunSpeed * _blackBoard.SpeedModifiler, _blackBoard.speedSmooth * Time.deltaTime);
            //Debug.Log(_blackBoard.nowSpeed / (_blackBoard.maxRunSpeed * _blackBoard.SpeedModifiler));
            _agent.Move(moveDir);
        }
        else
        {
            _blackBoard.nowSpeed = 0;
        }
        _agent.BlackBoard.anim.SetFloat("Speed", _blackBoard.nowSpeed / (_blackBoard.maxRunSpeed * _blackBoard.SpeedModifiler));
    }
}
