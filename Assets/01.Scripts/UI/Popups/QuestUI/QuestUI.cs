using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class QuestUI : UI_Popup
{
    [Header("Texts")]
    [SerializeField] protected string _codename;
    [SerializeField] protected TextMeshProUGUI _questNameText;
    [SerializeField] protected TextMeshProUGUI _questDescriptionText;
    [SerializeField] protected TextMeshProUGUI _questTaskCountText;

    public Quest Quest;

    public string CodeName => _codename;
    public TextMeshProUGUI QuestNameText => _questNameText;
    public TextMeshProUGUI QuestDescription => _questDescriptionText;

    public override void Awake()
    {
        base.Awake();
    }

    public abstract void UpdateUI(Quest binder);
}
