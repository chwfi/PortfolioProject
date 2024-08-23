using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using Unity.Collections;
using UnityEngine;

public class QuestSystem : MonoSingleton<QuestSystem>
{
    #region SavePath
    private const string kSaveRootPath = "questSystem";
    private const string kActiveQuestsSavePath = "activeQuests";
    private const string kCompletedQuestsSavePath = "completedQuests";
    private const string kActiveAchievementsSavePath = "activeAchievement";
    private const string kCompletedAchievementsSavePath = "completedAchievement";
    #endregion

    #region Events
    public delegate void QuestRegisteredHandler(Quest newQuest);
    public delegate void QuestCompletedHandler(Quest quest);
    public delegate void QuestCanceledHandler(Quest quest);
    #endregion

    private List<Quest> activeQuests = new();
    private List<Quest> completedQuests = new();

    private List<Quest> activeAchievements = new();
    private List<Quest> completedAchievements = new();

    [SerializeField] private QuestDatabase _questDatabase;
    [SerializeField] private QuestDatabase _achievementDatabase;

    public event QuestRegisteredHandler OnQuestRegistered;
    public event QuestCompletedHandler OnQuestCompleted;
    public event QuestCanceledHandler OnQuestCanceled;

    public event QuestRegisteredHandler OnAchievementRegistered;
    public event QuestCompletedHandler OnAchievementCompleted;

    public IReadOnlyList<Quest> ActiveQuests => activeQuests;
    public IReadOnlyList<Quest> CompletedQuests => completedQuests;
    public IReadOnlyList<Quest> ActiveAchievements => activeAchievements;
    public IReadOnlyList<Quest> CompletedAchievements => completedAchievements;

    private void Awake() 
    {
        if (!Load())
        {
            foreach (var achievement in _achievementDatabase.Quests)
                Register(achievement);
        }
    }

    private void OnApplicationQuit() {
        Save();
    }

    public Quest Register(Quest quest)
    {
        var newQuest = quest.Clone();

        if (newQuest is Achievement)
        {
            newQuest.OnCompleted += SetAchievementCompleted;

            activeAchievements.Add(newQuest);

            OnAchievementRegistered?.Invoke(newQuest);
            newQuest.OnRegister();
        }
        else
        {
            newQuest.OnCompleted += SetQuestCompleted;
            newQuest.OnCanceled += SetQuestCanceled;

            activeQuests.Add(newQuest);

            OnQuestRegistered?.Invoke(newQuest);
            newQuest.OnRegister();
        }

        return newQuest;
    }

    public void Report(object target, int successCount)
    {
        ReceiveReport(activeQuests, target, successCount);
        ReceiveReport(activeAchievements, target, successCount);
    }

    private void ReceiveReport(List<Quest> quests, object target, int successCount)
    {
        foreach (var quest in quests.ToArray())
            quest.ReceieveReport(target, successCount);
    }

    private void Save()
    {
        var root = new JObject
        {
            { kActiveQuestsSavePath, CreateSaveDatas(activeQuests) },
            { kCompletedQuestsSavePath, CreateSaveDatas(completedQuests) },
            { kActiveAchievementsSavePath, CreateSaveDatas(activeAchievements) },
            { kCompletedAchievementsSavePath, CreateSaveDatas(completedAchievements) }
        };

        PlayerPrefs.SetString(kSaveRootPath, root.ToString());
        PlayerPrefs.Save();
    }

    private bool Load()
    {
        if (PlayerPrefs.HasKey(kSaveRootPath))
        {
            var root = JObject.Parse(PlayerPrefs.GetString(kSaveRootPath));

            LoadSaveDatas(root[kActiveQuestsSavePath], _questDatabase, LoadActiveQuest);
            LoadSaveDatas(root[kCompletedQuestsSavePath], _questDatabase, LoadCompleteQuest);

            LoadSaveDatas(root[kActiveAchievementsSavePath], _achievementDatabase, LoadActiveQuest);
            LoadSaveDatas(root[kCompletedAchievementsSavePath], _achievementDatabase, LoadCompleteQuest);
            
            return true;
        }
        else
            return false;
    }

    private JArray CreateSaveDatas(IReadOnlyList<Quest> quests)
    {
        var saveDatas = new JArray();
        foreach (var quest in quests)
        {
            if (quest.IsSavable)
                saveDatas.Add(JObject.FromObject(quest.ToSaveData()));
        }
        return saveDatas;
    }

    private void LoadSaveDatas(JToken datasToken, QuestDatabase database, System.Action<QuestSaveData, Quest> onSuccess)
    {
        var datas = datasToken as JArray;
        foreach (var data in datas)
        {
            var saveData = data.ToObject<QuestSaveData>();
            var quest = database.FindQuestBy(saveData.codename);
            onSuccess.Invoke(saveData, quest);
        }
    }

    private void LoadActiveQuest(QuestSaveData saveData, Quest quest)
    {
        var newQuest = Register(quest);
        newQuest.LoadFrom(saveData);
    }

    private void LoadCompleteQuest(QuestSaveData saveData, Quest quest)
    {
        var newQuest = quest.Clone();
        newQuest.LoadFrom(saveData);

        if (newQuest is Achievement)
            completedAchievements.Add(newQuest);
        else
            completedQuests.Add(newQuest);
    }

    private void SetQuestCompleted(Quest quest)
    {
        activeQuests.Remove(quest);
        completedQuests.Add(quest);

        OnQuestCompleted?.Invoke(quest);
    }

    private void SetQuestCanceled(Quest quest)
    {
        activeQuests.Remove(quest);
        OnQuestCanceled?.Invoke(quest);

        Destroy(quest, Time.deltaTime);
    }

    private void SetAchievementCompleted(Quest achievement)
    {
        activeAchievements.Remove(achievement);
        activeAchievements.Add(achievement);

        OnAchievementCompleted?.Invoke(achievement);
    }
}
