using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FSMSubsequenceAction
{
    public FSMSubsequenceAction(FSMBaseAction action,FSMWorldState preconditions,int priority)
    {
        _action = action;
        _preconditions = preconditions;
        _priority = priority;
    }
    public FSMBaseAction _action;
    public FSMWorldState _preconditions;
    public int _priority;
}

public class FSMManager {

    Dictionary<E_ActionTYpe, FSMBaseAction> _actionSet = new Dictionary<E_ActionTYpe, FSMBaseAction>();
    Dictionary<E_ActionTYpe, List<FSMSubsequenceAction>> _nextActions = new Dictionary<E_ActionTYpe, List<FSMSubsequenceAction>>();

    Agent _agent;
    

    E_ActionTYpe _defaultAction;
    FSMBaseAction _currentAction;
    FSMWorldState _currentState;
    
    bool _isTransform;
    bool _isStop;

    public Agent Agent
    {
        get
        {
            return _agent;
        }
    }

    public FSMManager(Agent agent,E_ActionTYpe defaultAction)
    {
        _agent = agent;
        _defaultAction = defaultAction;
        _isStop = false;
        _isTransform = true;
    }
    /// <summary>
    /// 初始化状态机 
    /// </summary>
	public void Initialize()
    {
        foreach (FSMBaseAction action in _actionSet.Values)
        {
            action.BuildRelation();
        }
        _currentState = FSMWroldStateFactory.Creat();
        _currentState.Initialize();

        if (_actionSet.ContainsKey(_defaultAction))
        {
            _currentAction = _actionSet[_defaultAction];
            ActiveAction(_currentAction);
        }
        else
        {
            Debug.LogError("No This Action");
        }
        
    }
    
    public void Update()
    {
        if (_isStop) return;
        if (_currentAction != null)
        {
            if (_isTransform)
                TryGotoNextAction();
            _currentAction.Update();
        }
    }

    /// <summary>
    /// 添加Action
    /// </summary>
    /// <param name="action"></param>
    public void Addaction(FSMBaseAction action)
    {
        if (action == null) return;
        if (_actionSet.ContainsKey(action.GetActionType())) return;
        
        _actionSet.Add(action.GetActionType(), action);
    }

    /// <summary>
    /// 添加Action 切换关系
    /// </summary>
    /// <param name="nowState">前一个状态</param>
    /// <param name="nextState">下一个状态</param>
    /// <param name="preconditions">过度条件</param>
    /// <param name="priority">优先级</param>
    public void AddRelation(E_ActionTYpe nowState,E_ActionTYpe nextState,FSMWorldState preconditions,int priority)
    {
        if(_actionSet.ContainsKey(nowState) && _actionSet.ContainsKey(nextState))
        {
            FSMSubsequenceAction subAction = new FSMSubsequenceAction(_actionSet[nextState], preconditions, priority);

            if (!_nextActions.ContainsKey(nowState))
            {
                _nextActions.Add(nowState, new List<FSMSubsequenceAction>());
            }

            _nextActions[nowState].Add(subAction);
        }
    }

    public void SetStop(bool isStop)
    {
        _isStop = isStop;
    }
    public void SetTransform(bool isTransform)
    {
        _isTransform = isTransform;
    }

    /// <summary>
    /// 重置状态机
    /// </summary>
    public void Reset()
    {
        if (_currentAction != null)
        {
            DeactiveAction(_currentAction);
            _currentAction = null;
        }
        if (_actionSet.ContainsKey(_defaultAction))
            _currentAction = _actionSet[_defaultAction];
        if (_currentAction == null)
        {
            Debug.LogError("No this Action");
        }
        _isStop = false;
        _isTransform = true;
        _currentState = FSMWroldStateFactory.Creat();
    }

    /// <summary>
    /// 根据世界条件判断从当前Action 是否可以有过渡的Action 如果有就过渡
    /// </summary>
    void TryGotoNextAction()
    {
        if (!_nextActions.ContainsKey(_currentAction.GetActionType()))
        {
            return;
        }
        List<FSMSubsequenceAction> nextactions = _nextActions[_currentAction.GetActionType()];
        FSMSubsequenceAction tempAction = null;
        foreach(FSMSubsequenceAction sub in nextactions)
        {
            UpdateSubAction(sub, ref tempAction);
        }
        if (tempAction != null)
        {
            DeactiveAction(_currentAction);
            _currentAction = null;
            ActiveAction(tempAction._action);
            _currentAction = tempAction._action;
        }
    }

    /// <summary>
    /// 激活action
    /// </summary>
    /// <param name="action"></param>
    void ActiveAction(FSMBaseAction action)
    {
        if (_nextActions.ContainsKey(action.GetActionType()))
        {
            List<FSMSubsequenceAction> nextactions = _nextActions[action.GetActionType()];

            foreach(FSMSubsequenceAction next in nextactions)
            {
                next._action.ListenBegin();
            }
        }
        action.Active();
    }

    /// <summary>
    /// 失活action
    /// </summary>
    /// <param name="action"></param>
    void DeactiveAction(FSMBaseAction action)
    {
        if (_nextActions.ContainsKey(action.GetActionType()))
        {
            List<FSMSubsequenceAction> nextactions = _nextActions[action.GetActionType()];
            foreach(FSMSubsequenceAction next in nextactions)
            {
                next._action.ListenEnd();
            }
        }
        action.Deactive();
    }

    /// <summary>
    /// 更新当前Action的所有后继Action
    /// </summary>
    /// <param name="action"></param>
    /// <param name="tempAction"></param>
    /// <param name="priority"></param>
    void UpdateSubAction(FSMSubsequenceAction action,ref FSMSubsequenceAction tempAction)
    {
        action._action.Listen();
        if (CheckTransform(action))
        {
            if (tempAction == null || tempAction._priority < action._priority)
            {
                tempAction = action;
            }
        }
        
    }

    /// <summary>
    /// 检查该后继Action 是否满足转换条件
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    bool CheckTransform(FSMSubsequenceAction action)
    {
        if (_currentState.CheckContain(action._preconditions))
        {
            return true;
        }
        return false;
    }

    public void SetProperty(E_PropType type,object value)
    {
        _currentState.SetProperty(type, value);
    }

}
