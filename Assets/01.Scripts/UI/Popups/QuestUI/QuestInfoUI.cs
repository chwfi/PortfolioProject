using System;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

public class QuestInfoUI : QuestUI
{
    private List<TaskSuccessCountText> _taskSuccessCountTexts = new();

    public override void Awake()
    {
        base.Awake();
    }

    public override void AccessUI(bool active)
    {
        base.AccessUI(active);

        transform.SetAsFirstSibling();
    }

    public override void SetUI(Quest binder)
    {
        if (binder.State == QuestState.Complete)
        {
            _completedPanel.SetActive(true);
            return;
        }

        foreach (var task in binder.TaskGroup)
        {
            TaskSuccessCountText txt = Instantiate(_taskSuccessCountText, _countGroupTrm);
            txt.OwnTask = task;
            _taskSuccessCountTexts.Add(txt);
        }      
    }

    public override void UpdateUI(Quest binder)
    {
        if (binder.State == QuestState.Complete)
            _completedPanel.SetActive(true);

        foreach (var txt in _taskSuccessCountTexts)
        {
            txt.UpdateText();
        }

        _questNameText.text = binder.QuestName;
        _questDescriptionText.text = binder.QuestDescription;
    }
}
