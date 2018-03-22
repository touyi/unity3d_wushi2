using UnityEngine;
using System.Collections;

public class PlayerWalkAction : FSMBaseAction
{
    public PlayerWalkAction(Agent agent, FSMManager owner) : base(E_ActionTYpe.Walk, agent, owner)
    {
    }

    public override void BuildRelation()
    {
        FSMWorldState state = FSMWroldStateFactory.Creat();
        state.SetProperty(E_PropType.Speed, 1);
        _owner.AddRelation(E_ActionTYpe.Idle, E_ActionTYpe.Walk, state, 1);
    }

    public override void Active()
    {
        _agent.BlackBoard.anim.SetFloat("Speed", 1);
    }

    public override void Listen()
    {
        if (InputManager.GetInputVector2().magnitude>=1)
        {
            _owner.SetProperty(E_PropType.Speed, 1);
        }
    }

    public override void Update()
    {
        Vector2 move = InputManager.GetInputVector2();
        _agent.Move(new Vector3(move.x, 0f, move.y).normalized);
        base.Update();
    }
}
