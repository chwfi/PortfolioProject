using System.Collections.Generic;
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
    }

    public override void SetUI(Quest binder)
    {
        Debug.Log("SetUI");
        foreach (var task in binder.TaskGroup)
        {
            TaskSuccessCountText txt = Instantiate(_taskSuccessCountText, _countGroupTrm);
            txt.OwnTask = task;
            _taskSuccessCountTexts.Add(txt);
        }      
    }

    public override void UpdateUI(Quest binder)
    {
        Debug.Log("UpdateUI");
        foreach (var txt in _taskSuccessCountTexts)
        {
            txt.UpdateText();
        }

        _questNameText.text = binder.QuestName;
        _questDescriptionText.text = binder.QuestDescription;
        _questStateText.text = binder.State.ToString();
    }
}
