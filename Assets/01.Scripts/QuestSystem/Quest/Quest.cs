using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using System.Linq;

public enum QuestState
{
    Inactive,
    Running,
    Complete,
    Cancel,
    WaitingForCompletion
}

[CreateAssetMenu(menuName = "Quest/Quest")]
public class Quest : ScriptableObject
{
    #region Events
    public delegate void TaskSuccessChangedHandler(Quest quest, Task task, int currentSuccess, int prevSuccess);
    public delegate void CompletedHandler(Quest quest);
    public delegate void CanceldHandler(Quest quest);
    public delegate void NewTaskGroupHandler(Quest quest, TaskGroup currentTaskGroup, TaskGroup prevTaskGroup);
    #endregion

    [SerializeField] private Category _category;
    [SerializeField] private Sprite _icon;

    [Header("Text")]
    [SerializeField] private string _codeName;
    [SerializeField] private string _displayname;
    [SerializeField, TextArea] private string _description;

    [Header("Task")]
    [SerializeField] private TaskGroup[] _taskGroups;

    [Header("Condition")]
    [SerializeField] private QuestCondition _acceptCondition;

    [Header("Reward")] 
    [SerializeField] private Reward[] _rewards;

    [Header("Option")]
    [SerializeField] private bool _useAutoComplete;
    [SerializeField] private bool _isCancelable;
    [SerializeField] private bool _isSavable;

    private int _currentTaskGroupIndex;

    public Category Category => _category;
    public Sprite Icon => _icon;
    public string CodeName => _codeName;
    public string Displayname => _displayname;
    public string Description => _description;
    public QuestState State { get; private set; }
    public TaskGroup CurrentTaskGroup => _taskGroups[_currentTaskGroupIndex];
    public IReadOnlyList<TaskGroup> TaskGroups => _taskGroups;
    public IReadOnlyList<Reward> Rewards => _rewards;
    public bool IsRegistered => State != QuestState.Inactive;
    public bool IsCompletable => State == QuestState.WaitingForCompletion;
    public bool IsComplete => State == QuestState.Complete;
    public bool IsCancel => State == QuestState.Cancel;
    public virtual bool IsCancelable => _isCancelable;
    public bool IsAcceptable => _acceptCondition.IsPrepared(this);
    public virtual bool IsSavable => _isSavable;

    public event TaskSuccessChangedHandler onTaskSuccessChanged;
    public event CompletedHandler onCompleted;
    public event CanceldHandler onCanceled;
    public event NewTaskGroupHandler onNewTaskGroup;

    private List<GameObject> gameObjects;
    public List<GameObject> GameObjects => gameObjects;

    public void OnRegister()
    {
        Debug.Assert(!IsRegistered, "This quest has already been registered"); //Assert 코드는 디버깅이지만 빌드를 하면 자동으로 삭제되어 유용하다.
       
        foreach (var TaskGroup in _taskGroups)
        {
           TaskGroup.Setup(this);
           foreach (var task in TaskGroup.Tasks)
               task.onSuccessChanged += OnSuccessChanged;
        }
    }

    public void ReceieveReport(string category, object target, int successCount)
    {
        CheckIsRunning();

        if (IsComplete)
            return;

        CurrentTaskGroup.ReceiveReport(category, target, successCount);

        if (CurrentTaskGroup.IsAllTaskComplete)
        {
            if (_currentTaskGroupIndex + 1 == _taskGroups.Length)
            {
                State = QuestState.WaitingForCompletion;
                if (_useAutoComplete)
                    Complete();
            }
            else
            {
                var prevTaskGroup = _taskGroups[_currentTaskGroupIndex++];
                prevTaskGroup.End();
                CurrentTaskGroup.Start();
                onNewTaskGroup?.Invoke(this, CurrentTaskGroup, prevTaskGroup);
            }
        }
        else
            State = QuestState.Running;
    }

    public void Complete()
    {
        CheckIsRunning();

        Debug.Log("퀘스트 완료됨");

        foreach (var taskGroup in _taskGroups)
            taskGroup.Complete();

        State = QuestState.Complete;

        foreach (var reward in _rewards)
            reward.Give(this);

        onTaskSuccessChanged = null;
        onCompleted = null;
        onCanceled = null;
        onNewTaskGroup = null;
    }

    public virtual void Cancel()
    {
        CheckIsRunning();
        Debug.Assert(IsCancelable, "This quest can't be canceled");

        State = QuestState.Cancel;
        onCanceled?.Invoke(this);
    }

    public Quest Clone()
    {
        var clone = Instantiate(this);
        clone._taskGroups = _taskGroups.Select(x => new TaskGroup(x)).ToArray();

        return clone;
    }

    public QuestSaveData ToSaveData()
    {
        return new QuestSaveData
        {
            codeName = _codeName,
            state = State,
            taskGroupIndex = _currentTaskGroupIndex,
            taskSuccessCounts = CurrentTaskGroup.Tasks.Select(x => x.CurrentSuccess).ToArray()
        };
    }

    public void LoadFrom(QuestSaveData saveData)
    {
        State = saveData.state;
        _currentTaskGroupIndex = saveData.taskGroupIndex;

        for (int i = 0; i < _currentTaskGroupIndex; i++)
        {
            var taskGroup = _taskGroups[i];
            taskGroup.Start();
            taskGroup.Complete();
        }

        for (int i = 0; i < saveData.taskSuccessCounts.Length; i++)
        {
            CurrentTaskGroup.Start();
            CurrentTaskGroup.Tasks[i].CurrentSuccess = saveData.taskSuccessCounts[i];
        }
    }

    private void OnSuccessChanged(Task task, int currentSuccess, int prevSuccess)
        => onTaskSuccessChanged?.Invoke(this, task, currentSuccess, prevSuccess);

    [Conditional("UNITY_EDITOR")]
    private void CheckIsRunning()
    {
        Debug.Assert(IsRegistered, "This quest has already been registered");  
        Debug.Assert(!IsCancel, "This quest has been canceled");
        Debug.Assert(!IsCompletable, "This Quest has already been completed");
    }
}