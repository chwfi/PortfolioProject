using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestUI : UI_Popup
{
    [Header("Texts")]
    [SerializeField] protected TextMeshProUGUI _questNameText;
    [SerializeField] protected UnboundedTaskGroup _taskPrefab;
    [SerializeField] protected UnboundedRewardGroup _rewardPrefab;
    [SerializeField] protected GameObject _completedPanel;

    protected Transform _taskGroupTrm;
    protected Transform _rewardGroupTrm;

    private List<UnboundedTaskGroup> _taskTexts = new();
    private List<UnboundedRewardGroup> _rewardTexts = new();

    public override void Awake()
    {
        base.Awake();
    }

    public override void AccessUI(bool active)
    {
        base.AccessUI(active);
    }

    public void SetUI(Quest binder)
    {
        AccessUI(true);

        if (binder.State == QuestState.Complete)
        {
            _completedPanel.SetActive(true);
            return;
        }

        foreach (var task in binder.TaskGroup) // 작업들을 생성해서 UI에 불러옴. 여러개일 수도 있으므로 리스트로 하는것
        {
            UnboundedTaskGroup group = Instantiate(_taskPrefab, _taskGroupTrm);
            group.OwnTask = task;
            _taskTexts.Add(group);
        }      

        foreach (var reward in binder.Rewards) // 리워드 UI에 띄우는것. 이것도 위 로직과같음
        {
            UnboundedRewardGroup group = Instantiate(_rewardPrefab, _rewardGroupTrm);
            group.OwnReward = reward;
            _rewardTexts.Add(group);
        }
    }

    public void UpdateUI(Quest binder)
    {
        if (binder.State == QuestState.Complete)
        {  
            transform.SetAsLastSibling();
            _completedPanel.SetActive(true);
        }

        foreach (var txt in _taskTexts)
        {
            txt.UpdateText();
        }
        
        foreach (var txt in _rewardTexts)
        {
            txt.UpdateText();
        }

        _questNameText.text = binder.QuestName;
    }
}
