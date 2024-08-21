using System.Linq;
using UnityEngine;

public enum TaskState
{
    Inactive,
    Running,
    Complete
}

[CreateAssetMenu(menuName = "Quest/Task/Task", fileName = "Task_")]
public class Task : ScriptableObject
{
    public delegate void StateChangedHandler(Task task, TaskState currentState, TaskState prevState);
    public delegate void SuccessChangedHandler(Task task, int currentSuccess, int prevstate);

    [SerializeField] private Category _category;

    [Header("Text")]
    [SerializeField] private string _codeName;
    [SerializeField] private string _description;

    [Header("Action")]
    [SerializeField] private TaskAction _action;

    [Header("Target")]
    [SerializeField] private TaskTarget[] _targets;

    [Header("Setting")]
    [SerializeField] private InitialSuccessValue _initialSuccessValue; //�ʱ� ������
    [SerializeField] private int _needSuccessToComplete; //�������� �ʿ��� Ƚ��
    [SerializeField] private bool _canRecevieReportDuringCompletion; //������ �Ŀ��� ��� �������� �������̳�

    private TaskState _taskState;
    private int _currentSuccess;

    public event StateChangedHandler onStateChanged;
    public event SuccessChangedHandler onSuccessChanged;

    public int CurrentSuccess
    {
        get => _currentSuccess;
        set
        {
            int prevSuccess = _currentSuccess;
            _currentSuccess = Mathf.Clamp(value, 0, _needSuccessToComplete);
            if (_currentSuccess != prevSuccess)
            {
                State = _currentSuccess == _needSuccessToComplete ? TaskState.Complete : TaskState.Running;
                onSuccessChanged?.Invoke(this, _currentSuccess, prevSuccess);
            }
        }
    }
    public Category Category => _category;
    public string CodeName => _codeName;
    public string Description => _description;
    public int NeedSuccessToComplete => _needSuccessToComplete;
    public TaskState State
    {
        get => _taskState;
        set
        {
            var prevState = _taskState;
            _taskState = value;
            onStateChanged?.Invoke(this, _taskState, prevState);
        }
    }
    public bool IsComplete => State == TaskState.Complete;
    public Quest Owner { get; private set; }
     
    public void Setup(Quest owner)
    {
        Owner = owner;
    }

    public void Start()
    {
        State = TaskState.Running;
        if (_initialSuccessValue)
            CurrentSuccess = _initialSuccessValue.GetValue(this);
        else
            CurrentSuccess = 0;
    }

    public void End()
    {
        onStateChanged = null;
        onSuccessChanged = null;
    }
    
    public void ReceieveReport(int successCount)
    {
        CurrentSuccess = _action.Run(this, CurrentSuccess, successCount);
    }

    public void Complete()
    {
        CurrentSuccess = _needSuccessToComplete;
    }

    public bool IsTarget(string category, object target)
        => Category == category &&
        _targets.Any(x => x.IsEqual(target)) &&
        (!IsComplete || (IsComplete && _canRecevieReportDuringCompletion));
}
