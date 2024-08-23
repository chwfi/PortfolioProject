using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum TaskGroupState
{
    Inactive,
    Active,
    Complete
}

[System.Serializable]
public class TaskGroup
{
    [SerializeField] private Task[] _tasks;

    public IReadOnlyList<Task> Tasks => _tasks;
    public Quest Owner { get; private set; } 
    public bool IsAllTaskComplete => _tasks.All(x => x.IsComplete);
    public bool IsComplete => State == TaskGroupState.Complete;
    public TaskGroupState State { get; private set; }

    public TaskGroup(TaskGroup copyTarget)
    {
        _tasks = copyTarget.Tasks.Select(x => Object.Instantiate(x)).ToArray();
    }

    public void SetOwner(Quest owner)
    {
        Owner = owner;
        foreach (var task in _tasks)
            task.SetOwner(owner);
    }

    public void Start()
    {
        State = TaskGroupState.Active;
        foreach (var task in _tasks)
            task.Start();
    }

    public void End()
    {
        State = TaskGroupState.Complete;
        foreach (var task in _tasks)
            task.End();
    }

    public void ReceiveReport(object target, int successCount)
    {
        foreach (var task in _tasks)
        {
            task.ReceieveReport(successCount);
        }
    }

    public void CompleteImmediately()
    {
        if (IsComplete)
            return;

        State = TaskGroupState.Complete;

        foreach (var task in Tasks)
        {
            if (!task.IsComplete)
                task.CompleteImmediately();
        }
    }
}
