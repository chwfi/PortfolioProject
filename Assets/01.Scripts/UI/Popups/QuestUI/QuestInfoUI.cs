using System;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

public class QuestInfoUI : QuestUI
{
    //[Header("Quest Button")]

    private List<TaskSuccessCountText> _taskSuccessCountTexts = new();

    public override void Awake()
    {
        base.Awake();
    }

    public override void AccessUI(bool active)
    {
        base.AccessUI(active);
    }

    public override void SetUI(Quest binder)
    {
        foreach (var task in binder.TaskGroup)
        {
            TaskSuccessCountText txt = Instantiate(_taskSuccessCountText, _countGroupTrm);
            txt.OwnTask = task;
            _taskSuccessCountTexts.Add(txt);
        }      
    }

    public override void UpdateUI(Quest binder)
    {
        foreach (var txt in _taskSuccessCountTexts)
        {
            txt.UpdateText();
        }

        switch (binder.State)
        {
            case QuestState.Inactive:
                _questStateText.text = String.Empty;
                break;
            case QuestState.Active:
                _questStateText.text = "퀘스트 진행 중";
                break;
            case QuestState.Complete:
                _questStateText.text = "퀘스트 완료됨";
                break;    
        }

        _questNameText.text = binder.QuestName;
        _questDescriptionText.text = binder.QuestDescription;
    }
}
