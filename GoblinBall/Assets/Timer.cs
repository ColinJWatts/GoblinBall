using UnityEngine;
using System.Collections;

public class Timer 
{
    public int Counter { get { return _counter; } set { _counter = value; } }
    public int Id { get { return _id; } }
    public string Description { get { return _description; } }

    private int _counter;
    private int _id;
    private string _description = "Unknown Timer";

    public delegate void OnFinishDelegate();
    private OnFinishDelegate OnFinish;

    public delegate void UpdateDelegate(ref int counter);
    private UpdateDelegate Update;

    private void DefaultUpdate(ref int counter)
    {
        counter--;
    }

    public Timer(string description, int counter, OnFinishDelegate onFinish, UpdateDelegate update = null)
    {
        _description = description;
        _counter = counter;
        OnFinish = onFinish;
        if (update != null)
        {
            Update = update;
        }
        else
        {
            Update = DefaultUpdate;
        }
        _id = GameManager.instance.GetNewTimerId();
        GameManager.instance.TimerList.Add(_id, this);
    }

    public Timer(int counter, OnFinishDelegate onFinish, UpdateDelegate update = null)
    {
        _counter = counter;
        OnFinish = onFinish;
        if(update != null)
        {
            Update = update;
        }
        else
        {
            Update = DefaultUpdate;
        }
        _id = GameManager.instance.GetNewTimerId();
        GameManager.instance.TimerList.Add(_id, this);
    }
    
    public void DoUpdate()
    {
        Update(ref _counter);
        if(_counter <= 0)
        {
            OnFinish();
            GameManager.instance.ToRemove.Add(_id);
        }
    }
}
