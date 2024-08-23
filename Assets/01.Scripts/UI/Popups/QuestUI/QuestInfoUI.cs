
using System.Collections.Generic;
using UnityEngine;

public class QuestInfoUI : QuestUI
{
    private List<TaskSuccessCountText> _taskSuccessCountTexts = new();
    private Dictionary<TaskSuccessCountText, Task> _taskDataDictionary = new();

    public override void Awake()
    {
        base.Awake();
    }

    public override void AccessUI(bool active)
    {
        base.AccessUI(active);
    }

    public override void UpdateUI(Quest binder)
    {
        if (binder.State == QuestState.Inactive)
        {
            foreach (var task in binder.TaskGroup.Tasks)
            {
                TaskSuccessCountText txt = Instantiate(_taskSuccessCountText, _countGroupTrm);
                _taskSuccessCountTexts.Add(txt);
                _taskDataDictionary.Add(txt, task);
            }
        }

        foreach (var txt in _taskSuccessCountTexts)
        {
            _taskDataDictionary.TryGetValue(txt, out Task task);
            txt.UpdateText(task.CurrentSuccessValue, task.NeedToSuccessValue);
        }

        _questNameText.text = binder.QuestName;
        _questDescriptionText.text = binder.QuestDescription;
        _questStateText.text = binder.State.ToString();
    }
}
