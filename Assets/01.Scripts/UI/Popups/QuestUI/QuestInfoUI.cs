using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestInfoUI : QuestUI
{
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
        Debug.Log("재발");
        _questNameText.text = binder.QuestName;
        _questDescriptionText.text = binder.QuestDescription;
    }
}
