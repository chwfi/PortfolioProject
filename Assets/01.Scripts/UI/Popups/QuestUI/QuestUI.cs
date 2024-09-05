using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class QuestUI : UI_Popup
{
    [Header("Texts")]
    [SerializeField] protected int _codename;
    [SerializeField] protected TextMeshProUGUI _questNameText;
    [SerializeField] protected UnboundedTaskGroup _taskPrefab;
    [SerializeField] protected UnboundedRewardGroup _rewardPrefab;
    [SerializeField] protected GameObject _completedPanel;

    protected Transform _taskGroupTrm;
    protected Transform _rewardGroupTrm;

    public int CodeName => _codename;

    public override void Awake()
    {
        base.Awake();

        _taskGroupTrm = transform.Find("TaskLayoutGroup").transform;
        _rewardGroupTrm = transform.Find("RewardPanel/RewardLayoutGroup").transform;
    }

    public abstract void SetUI(Quest binder);

    public abstract void UpdateUI(Quest binder);
}
