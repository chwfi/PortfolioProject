using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestRegisterer : MonoSingleton<QuestRegisterer>
{
    [SerializeField] private Quest[] _quests;
    
    private void Start() 
    {
        var questSystem = QuestSystem.Instance;

        if (!questSystem.IsFileExist) // Json파일이 존재하지 않으면 처음 실행이므로 퀘스트들 등록해준다
        {
            foreach (var quest in _quests)
            {
                questSystem.Register(quest);
            }
        }
    }
}