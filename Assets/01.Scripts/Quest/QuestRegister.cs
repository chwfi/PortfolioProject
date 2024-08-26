using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestRegister : MonoBehaviour
{
    [SerializeField] private Quest[] _quest;

    private void Start() 
    {
        var questSystem = QuestSystem.Instance;

        // 새로운 퀘스트를 등록할지 결정
        if (!questSystem.IsFileExist)
        {
            Debug.Log("Registering new quest");
            for (int i = 0; i < _quest.Length; i++)
            {
                if (CheckExist(_quest[i]))
                {
                    questSystem.Register(_quest[i]);
                }
            }
        }
    }

    private bool CheckExist(Quest quest)
    {
        return QuestSystem.Instance.IsQuestActive(quest.CodeName);
    }
}