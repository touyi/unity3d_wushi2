using UnityEngine;
using System.Collections;

public class PlayerIdleAction : FSMBaseAction
{

    public PlayerIdleAction(Agent agent,FSMManager owner) : base(E_ActionTYpe.Idle,agent,owner)
    {

    }
    public override void BuildRelation()
    {
        FSMWorldState state = FSMWroldStateFactory.Creat();

        state.SetProperty(E_PropType.Speed, 0);

        _owner.AddRelation(E_ActionTYpe.Walk, E_ActionTYpe.Idle, state, 1);
    }

    public override void Active()
    {
        _agent.BlackBoard.anim.SetFloat("Speed", 0);
    }
    public override void Listen()
    {
        Vector2 move = InputManager.GetInputVector2();
        if(move.magnitude<0.2f)
        {
            _owner.SetProperty(E_PropType.Speed, 0);
        }
    }

}
