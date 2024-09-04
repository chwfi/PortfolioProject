using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Quest/QuestGroup")]
public class QuestRegisterGroup : ScriptableObject
{
    public Quest[] Quests;

    public void OnRegisterQuests()
    {
        for (int i = 0; i < Quests.Length; i++)
        {
            Quests[i].OnRegister();
            Quests[i].CodeName = i;
        }
    }
}
