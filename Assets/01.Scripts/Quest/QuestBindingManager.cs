using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class QuestBindingManager : MonoSingleton<QuestBindingManager>
{
    private PopupUIController _questUIController;
    private List<QuestUI> _uiList = new();

    private void Awake() {
        var questSystem = QuestSystem.Instance;
        questSystem.OnQuestRegistered += SetQuestDictionary;

        _questUIController = FindObjectOfType<PopupUIController>();
        _uiList.AddRange(_questUIController.transform.GetComponentsInChildren<QuestUI>());
    }

    private void SetQuestDictionary(Quest quest) 
    {
        foreach (var ui in _uiList)
        {
            if (ui.CodeName == quest.CodeName)
                quest.OnUIUpdate += ui.UpdateUI;
        }
    }
}
