using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTester : MonoBehaviour
{
    [SerializeField] private Quest[] _quests;
    [SerializeField] private TaskTarget _target;

    private void Start() 
    {
        var questSystem = QuestSystem.Instance;

        questSystem.OnQuestRegistered += (_quest) =>
        {
            Debug.Log($"New Quest: {_quest.CodeName} Registered");
            Debug.Log($"Active Quests Count: {questSystem.ActiveQuests.Count}");
        }; 

        questSystem.OnQuestCompleted += (_quest) =>
        {
            Debug.Log($"Quest: {_quest.CodeName} Completed");
            Debug.Log($"Completed Quests Count: {questSystem.CompletedQuests.Count}");
        };

        for (int i = 0; i < _quests.Length; i++)
        {
            var newQuest = questSystem.Register(_quests[i]);
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            QuestSystem.Instance.Report(_target, 1);
        }
    }
}
