using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class QuestUI : UI_Popup
{
    [Header("Texts")]
    [SerializeField] protected int _codename;
    [SerializeField] protected TextMeshProUGUI _questNameText;
    [SerializeField] protected TextMeshProUGUI _questDescriptionText;
    [SerializeField] protected TaskSuccessCountText _taskSuccessCountText;
    [SerializeField] protected GameObject _completedPanel;

    protected Transform _countGroupTrm;

    public int CodeName => _codename;

    public override void Awake()
    {
        base.Awake();

        _countGroupTrm = transform.Find("SuccessCountLayoutGroup").transform;
    }

    public abstract void SetUI(Quest binder);

    public abstract void UpdateUI(Quest binder);
}
