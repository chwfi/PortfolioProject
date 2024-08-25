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
    [SerializeField] private List<Task> _tasks;

    public List<Task> Tasks => _tasks;
    public Quest Owner { get; private set; } 
    public bool IsAllTaskComplete => _tasks.All(x => x.IsComplete);
    public bool IsComplete => State == TaskGroupState.Complete;
    public TaskGroupState State { get; private set; }

    public void SetOwner(Quest owner)
    {
        Owner = owner;
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

    public void ReceiveReport(object target, int successCount, Quest quest)
    {
        foreach (var task in _tasks)
        {
            task.ReceieveReport(target, successCount, quest);
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
