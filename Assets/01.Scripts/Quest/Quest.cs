using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum QuestState
{
    Inactive,
    Active,
    Complete,
    Cancel,
    WaitingForCompletion
}

[CreateAssetMenu(menuName = "SO/Quest/Quest")]
public class Quest : ScriptableObject
{
    public delegate void CompletedHandler(Quest quest);
    public delegate void CanceldHandler(Quest quest);
    public delegate void UpdateUIHandler(Quest quest);

    [SerializeField] private string _codeName;

    [Header("Info")]
    [SerializeField] private string _binderCodeName;
    [SerializeField] private string _questName;
    [SerializeField] private Sprite _questIcon;
    [SerializeField] private string _questDescription;

    [Header("TaskGroup")]
    [SerializeField] private TaskGroup _taskGroup;

    [Header("Reward")]
    [SerializeField] private Reward[] _rewards;

    [Header("Option")]
    [SerializeField] private bool _isAutoComplete;
    [SerializeField] private bool _isCanclable;
    [SerializeField] private bool _isSavable;

    public event CompletedHandler OnCompleted;
    public event CanceldHandler OnCanceled;
    public Guid id = Guid.NewGuid();
    public event UpdateUIHandler OnUIUpdate;

    public TaskGroup TaskGroup => _taskGroup;
    public string CodeName => _codeName;
    private QuestState _state;
    public QuestState State
    {
        get => _state;
        set
        {
            _state = value;
            OnUIUpdate?.Invoke(this);
        }
    }
    public string BinderCodeName => _binderCodeName;
    public string QuestName => _questName;
    public Sprite QuestIcon => _questIcon;
    public string QuestDescription => _questDescription;

    public IReadOnlyList<Reward> Rewards => _rewards;
    public bool IsRegistered => State != QuestState.Inactive;
    public bool IsCompletable => State == QuestState.WaitingForCompletion;
    public bool IsComplete => State == QuestState.Complete;
    public bool IsCancel => State == QuestState.Cancel;
    public virtual bool IsCancelable => _isCanclable;
    public virtual bool IsSavable => _isSavable;

    public void OnRegister()
    {
        Debug.Assert(!IsRegistered, "This quest has already been registered"); //Assert 코드는 디버깅이지만 빌드를 하면 자동으로 삭제되어 유용하다.

        _taskGroup.SetOwner(this);
        _taskGroup.Start();
        OnUIUpdate?.Invoke(this);
    }

    public void ReceieveReport(object target, int successCount)
    {
        if (IsComplete)
            return;

        _taskGroup.ReceiveReport(target, successCount);

        if (_taskGroup.IsAllTaskComplete)
        {
            State = QuestState.WaitingForCompletion;
            if (_isAutoComplete)
                Complete();
        }
        else
            State = QuestState.Active;
    }

    public void Complete()
    {
        Debug.Log("퀘스트 완료됨");

        _taskGroup.CompleteImmediately();

        State = QuestState.Complete;
        OnCompleted?.Invoke(this);

        foreach (var reward in _rewards)
            reward.Give(this);

        OnCompleted = null;
        OnCanceled = null;
    }

    public virtual void Cancel()
    {
        Debug.Assert(IsCancelable, "This quest can't be canceled");

        State = QuestState.Cancel;
        OnCanceled?.Invoke(this);
    }

    public Quest Clone()
    {
        var clone = Instantiate(this);
        clone._taskGroup = _taskGroup;

        return clone;
    }

    public QuestSaveData ToSaveData()
    {
        return new QuestSaveData
        {
            codename = _codeName,
            //binder = _questBinder,
            state = State,
            taskSuccessCounts = _taskGroup.Tasks.Select(x => x.CurrentSuccessValue).ToArray()
        };
    }

    public void LoadFrom(QuestSaveData saveData)
    {
        State = saveData.state;

        // for (int i = 0; i < _currentTaskGroupIndex; i++)
        // {
        //     var taskGroup = _taskGroups[i];
        //     taskGroup.Start();
        //     taskGroup.Complete();
        // }

        for (int i = 0; i < saveData.taskSuccessCounts.Length; i++)
        {
            _taskGroup.Start();
            _taskGroup.Tasks[i].CurrentSuccessValue = saveData.taskSuccessCounts[i];
        }
    }
}
