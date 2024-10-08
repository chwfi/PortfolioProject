using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(menuName = "SO/QuestDatabase")]
public class QuestDatabase : ScriptableObject // 퀘스트들을 담아놓는 데이터베이스. 다른 곳에서 찾아 빼내 쓴다.
{
    [SerializeField] private List<Quest> _quests;
    public List<Quest> Quests => _quests;

    public Quest FindQuestBy(int codeName) => _quests.FirstOrDefault(x => x.CodeName == codeName);

    #if UNITY_EDITOR
    [ContextMenu("FindQuests")]
    private void FindQuests()
    {
        FindQuestBy<Quest>();
    }

    private void FindQuestBy<T>() where T : Quest
    {
        _quests = new List<Quest>();

        string[] guids = AssetDatabase.FindAssets($"t:{typeof(T)}");
        foreach (var guid in guids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            var quest = AssetDatabase.LoadAssetAtPath<T>(assetPath);

            if (quest.GetType() == typeof(T))
                _quests.Add(quest);

            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
        }
    }
    #endif
}
