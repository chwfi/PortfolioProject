using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public enum QuestState
{
    Inactive,
    Active,
    Complete,
    Cancel,
    WaitingForCompletion,
    None
}

[CreateAssetMenu(menuName = "SO/Quest/Quest")]
public class Quest : ScriptableObject
{
    public delegate void CompletedHandler(Quest quest);
    public delegate void CanceldHandler(Quest quest);
    public delegate void SetUIHandler(Quest quest);
    public delegate void UpdateUIHandler(Quest quest);

    [Header("Info")]
    [SerializeField] private int _codeName;
    [SerializeField] private string _questName;
    [SerializeField] private Sprite _questIcon;
    [SerializeField] private string _questDescription;

    [Header("TaskGroup")]
    [SerializeField] private Task[] _taskGroup;

    [Header("Reward")]
    [SerializeField] private Reward[] _rewards;

    [Header("Option")]
    [SerializeField] private bool _isAutoStartQuest;
    [SerializeField] private bool _isAutoComplete;
    [SerializeField] private bool _isCanclable;
    [SerializeField] private bool _isSavable;

    public event CompletedHandler OnCompleted;
    public event CanceldHandler OnCanceled;
    public event SetUIHandler OnSetUI;
    public event UpdateUIHandler OnUpdateUI;

    public Task[] TaskGroup => _taskGroup;
    public int CodeName
    {
        get => _codeName;
        set => _codeName = value;
    }
    private QuestState _state;
    public QuestState State
    {
        get => _state;
        set
        {
            _state = value;
            UpdateUI();
        }
    }
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
    public bool IsAllTaskComplete => _taskGroup.All(x => x.IsComplete);

    private QuestUI _questUI;

    public void OnRegister()
    {
        Debug.Assert(!IsRegistered, "This quest has already been registered"); //Assert 코드는 디버깅이지만 빌드를 하면 자동으로 삭제되어 유용하다.

        if (_isAutoStartQuest)
            State = QuestState.Active;    

        foreach (var task in _taskGroup)
        {
            task.SetOwner(this);
            task.Start();
        }

        _questUI = QuestBindingManager.Instance.SetUI(this);
        SetUI();
    }

    public void OnReceieveReport(object target, int successCount)
    {
        if (IsComplete)
            return;

        foreach (var task in _taskGroup)
            task.ReceieveReport(target, successCount, this);

        State = QuestState.Active;
    }

    public void OnCheckComplete()
    {
        if (IsAllTaskComplete)
        {
            if (_isAutoComplete)
                Complete();
        }
    }

    public void Complete()
    {
        var questSystem = QuestSystem.Instance;
        State = QuestState.Complete;

        OnCompleted?.Invoke(this);

        foreach (var reward in _rewards)
            reward.Give(this);

        questSystem.OnQuestRecieved -= OnReceieveReport;
        questSystem.OnCheckCompleted -= OnCheckComplete;
        questSystem.OnUpdateQuestUI -= UpdateUI;
        questSystem.OnSetQuestUI -= SetUI;

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

        var questSystem = QuestSystem.Instance;
        
        questSystem.OnQuestRecieved += clone.OnReceieveReport;
        questSystem.OnCheckCompleted += clone.OnCheckComplete;
        questSystem.OnUpdateQuestUI += clone.UpdateUI;
        questSystem.OnSetQuestUI += clone.SetUI;

        return clone;
    }

    public QuestSaveData ToSaveData()
    {
        return new QuestSaveData
        {
            codeName = _codeName,
            state = _state,
            taskSaveData = _taskGroup.Select(task => new TaskSaveData
            {
                currentSuccess = task.CurrentSuccessValue
            }).ToArray()
        };
    }

    public void LoadFrom(QuestSaveData saveData)
    {
        _state = saveData.state;

        for (int i = 0; i < _taskGroup.Length; i++)
        {
            if (i < saveData.taskSaveData.Length)
            {
                _taskGroup[i].CurrentSuccessValue = saveData.taskSaveData[i].currentSuccess;
            }
        }
    }

    #region EventMethods
    public void UpdateUI()
    {
        if (_questUI == null) return;

        OnUpdateUI?.Invoke(this);
    }

    public void SetUI()
    {
        if (_questUI == null) return;

        OnSetUI?.Invoke(this);
        OnUpdateUI?.Invoke(this);
    }
    #endregion
}