using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(menuName = "SO/QuestDatabase")]
public class QuestDatabase : ScriptableObject
{
    [SerializeField] private List<Quest> _quests;
    public IReadOnlyList<Quest> Quests => _quests;

    public Quest FindQuestBy(int codeName) => _quests.FirstOrDefault(x => x.CodeName == codeName);

    #if UNITY_EDITOR
    [ContextMenu("FindQuests")]
    private void FindQuests()
    {
        FindQuestBy<Quest>();
    }

    [ContextMenu("FindAchievements")]
    private void FindAchievements()
    {
        FindQuestBy<Achievement>();
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
