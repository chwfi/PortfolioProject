using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public enum TaskGroupState
{
    Inactive,
    Running,
    Complete
}

[System.Serializable]
public class TaskGroup
{
    [SerializeField]
    private Task[] _tasks; //Task���� ����

    public IReadOnlyList<Task> Tasks => _tasks;
    public Quest Owner { get; private set; } 
    public bool IsAllTaskComplete => _tasks.All(x => x.IsComplete);
    public bool IsComplete => State == TaskGroupState.Complete;
    public TaskGroupState State { get; private set; }

    public TaskGroup(TaskGroup copyTarget)
    {
        _tasks = copyTarget.Tasks.Select(x => Object.Instantiate(x)).ToArray();
    }

    public void Setup(Quest owner) //������ ����
    {
        Owner = owner;
        foreach (var task in _tasks)
            task.Setup(owner);
    }

    public void Start()
    {
        State = TaskGroupState.Running;
        foreach (var task in _tasks)
            task.Start();
    }

    public void End()
    {
        State = TaskGroupState.Complete;
        foreach (var task in _tasks)
            task.End();
    }

    public void ReceiveReport(string category, object target, int successCount)
    {
        foreach (var task in _tasks)
        {
            if (task.IsTarget(category, target))
                task.ReceieveReport(successCount);
        }
    }

    public void Complete()
    {
        if (IsComplete)
            return;

        State = TaskGroupState.Complete;

        foreach (var task in Tasks)
        {
            if (!task.IsComplete)
                task.Complete();
        }
    }
}
