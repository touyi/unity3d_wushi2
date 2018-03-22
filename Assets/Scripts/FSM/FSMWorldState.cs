using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class FSMWorldProp
{
    public E_PropType _stateKey;
    public object _value;

    public FSMWorldProp(E_PropType type,Object value)
    {
        _stateKey = type;
        _value = value;
    }
    public bool Equals(FSMWorldProp prop)
    {
        if (prop == null) return false;
        if (prop._stateKey == _stateKey)
        {
            return prop._value.Equals(_value);
        }
        return false;
    }
}
public static class FSMWroldStateFactory
{
    static Queue<FSMWorldState> _states = new Queue<FSMWorldState>();
    public static FSMWorldState Creat()
    {
        FSMWorldState temp = null;
        if (_states.Count > 0)
        {
            temp = _states.Dequeue();
            temp.Initialize();
        }
        temp =  new FSMWorldState();
        temp.Initialize();
        return temp;
    }
    public static void Return(FSMWorldState state)
    {
        if (state != null)
        {
            _states.Enqueue(state);
        }
    }
}

public class FSMWorldState
{
    FSMWorldProp[] _stateSet = new FSMWorldProp[(int)E_PropType.Count];
    public FSMWorldState()
    {
        Reset();
    }
    public void Initialize()
    {
        Reset();
    }
    public void Reset()
    {
        for(int i = 0; i < _stateSet.Length; i++)
        {
            _stateSet[i] = null;
        }
    }

    public void SetProperty(E_PropType type,object value)
    {
        _stateSet[(int)type] = new FSMWorldProp(type,value);
    }
    public object GetProperty(E_PropType type)
    {
        int index = (int)type;
        if (_stateSet[index] != null) return _stateSet[index]._value;
        return null;
    }

    public bool CheckContain(FSMWorldState state)
    {
        int maxID = (int)E_PropType.Count;
        for (int i = 0; i < maxID; i++)
        {
            if (state._stateSet[i]!=null)
            {
                if (!state._stateSet[i].Equals(_stateSet[i]))
                {
                    return false;
                }
            }
        }
        return true;
    }
}
