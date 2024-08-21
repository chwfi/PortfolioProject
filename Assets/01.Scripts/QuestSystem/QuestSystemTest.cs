using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestSystemTest : MonoBehaviour
{
    [SerializeField] private Quest _quest;
    [SerializeField] private Category _category;
    [SerializeField] private TaskTarget _target;

    private void Start() 
    {
        var questSystem = QuestSystem.Instance;

        questSystem.OnQuestRegistered += (_quest) =>
        {
            Debug.Log($"New Quest:{_quest.CodeName} Registered");
            Debug.Log($"Active Quests Count : {questSystem.ActiveQuests.Count}");
        };

        questSystem.OnQuestCompleted += (_quest) =>
        {
            Debug.Log($"Quest:{_quest.CodeName} Completed");
            Debug.Log($"Completed Quests Count : {questSystem.CompletedQuests.Count}");
        };

        var newQuest = questSystem.Register(_quest);
        newQuest.onTaskSuccessChanged += (quest, task, currentSuccess, prevSuccess) =>
        {
            Debug.Log($"Quest:{quest.CodeName}, Task:{task.CodeName}, CurrentSuccess:{currentSuccess}");
        };
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space))
            QuestSystem.Instance.ReceiveReport(_category, _target, 1);
    }
}
