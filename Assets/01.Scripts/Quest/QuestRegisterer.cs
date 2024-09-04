using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestRegisterer : MonoSingleton<QuestRegisterer>
{
    [SerializeField] private Quest[] _questGroups;
    public Quest[] QuestGroups => _questGroups;

    public int CurrentQuestsCount = 0;
    public List<QuestUI> QuestUIList;

    private void Start() 
    {
        var questSystem = QuestSystem.Instance;

        if (!questSystem.IsFileExist) // Json파일이 존재하지 않으면 처음 실행이므로 퀘스트들 등록해준다
        {
            foreach (var group in _questGroups)
            {
                questSystem.Register(group);
            }
        }

        QuestUIList[0].AccessUI(true);
    }

    public void SetCurrentQuest(Quest quest)
    {
        QuestUIList[CurrentQuestsCount].AccessUI(true);
        CurrentQuestsCount++;
    }
}