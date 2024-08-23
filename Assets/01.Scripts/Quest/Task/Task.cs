using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TaskState
{
    Inactive,
    Active,
    Complete
}

[CreateAssetMenu(menuName = "SO/Quest/Task", fileName = "Task_")]
public class Task : ScriptableObject
{
    public delegate void StateChangedHandler(Task task, TaskState currentState, TaskState prevState);

    [Header("Text")]
    [SerializeField] private string _description;

    [Header("Target")]
    [SerializeField] private TaskTarget _taskTarget;

    [Header("Setting")]
    [SerializeField] private int _initialSuccessValue = 0;
    [SerializeField] private int _needToSuccessValue;

    public event StateChangedHandler OnStateChanged;

    private TaskState _taskState;
    private int _currentSuccessValue;

    public int CurrentSuccessValue
    {
        get => _currentSuccessValue;
        set => _currentSuccessValue = value;
    }
    public int NeedToSuccessValue => _needToSuccessValue;
    public TaskState TaskState
    {
        get => _taskState;
        set
        {
            var prevState = _taskState;
            _taskState = value;
            OnStateChanged?.Invoke(this, _taskState, prevState);
        }
    }

    public bool IsComplete => TaskState == TaskState.Complete;
    public Quest Owner { get; private set; }
     
    public void SetOwner(Quest owner)
    {
        Owner = owner;
    }

    public void Start()
    {
        TaskState = TaskState.Active;
        _currentSuccessValue = _initialSuccessValue;
    }

    public void End()
    {
        OnStateChanged = null;
    }
    
    public void ReceieveReport(int successCount)
    {
        _currentSuccessValue += successCount;
            Debug.Log(_currentSuccessValue);

        if (_currentSuccessValue >= _needToSuccessValue)
        {
            TaskState = TaskState.Complete;
        }
    }

    public bool IsTargetEqual(object target)
    {
        if (_taskTarget.IsTargetEqual(target))
            return true;
        else
            return false;
    }

    public void CompleteImmediately()
    {
        _currentSuccessValue = _needToSuccessValue;
    }
}
