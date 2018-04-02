using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

public static class FSMActionFactory
{

    public static FSMBaseAction Create(E_ActionTYpe type,FSMManager owner)
    {
            return CreateAction(type, owner.Agent, owner);
    }
    private static FSMBaseAction CreateAction(E_ActionTYpe type, Agent agent, FSMManager owner)
    {
        switch (type)
        {
            case E_ActionTYpe.Idle:
                return new PlayerIdleAction(agent, owner);
            case E_ActionTYpe.Walk:
                return new PlayerWalkAction(agent, owner);
            //case E_ActionTYpe.Attack:

            //    break;
            default:
                Debug.LogError("No this Action");
                return null;
        }

    }


}

public class FSMBaseAction {
    protected E_ActionTYpe _actionType;
    protected Agent _agent;
    protected FSMManager _owner;
    protected float _time;

    public FSMBaseAction(E_ActionTYpe type,Agent agent,FSMManager owner)
    {
        _actionType = type;
        _agent = agent;
        _owner = owner;
        Initialize();
    }


    /// <summary>
    /// 作为后继状态时候监听每一帧世界状态
    /// </summary>
    public virtual void Listen()
    {

    }

    /// <summary>
    /// 开始作为当前状态的后继状态
    /// </summary>
    public virtual void ListenBegin()
    {

    }

    /// <summary>
    /// 结束作为当前状态的后继状态
    /// </summary>
    public virtual void ListenEnd()
    {

    }

    /// <summary>
    /// 激活Action
    /// </summary>
    public virtual void Active()
    {
        _time = 0;
    }
    /// <summary>
    /// 失活Action
    /// </summary>
    public virtual void Deactive()
    {
    }
    public virtual void Update()
    {
        _time += Time.deltaTime;
    }
    /// <summary>
    /// 初始化Action
    /// </summary>
    public virtual void Initialize()
    {

    }
    /// <summary>
    /// 重置Action
    /// </summary>
    public virtual void Reset(FSMManager fsm)
    {
        _owner = fsm;
        _agent = fsm.Agent;
        Initialize();
    }

    public E_ActionTYpe GetActionType()
    {
        return _actionType;
    }

    /// <summary>
    /// 建立后继关系
    /// </summary>
    public virtual void BuildRelation()
    {

    }
    
}

