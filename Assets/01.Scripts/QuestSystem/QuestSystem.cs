using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
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
            newQuest.onCompleted += SetAchievementCompleted;

            activeAchievements.Add(newQuest);

            newQuest.OnRegister();
            OnAchievementRegistered?.Invoke(newQuest);
        }
        else
        {
            newQuest.onCompleted += SetQuestCompleted;
            newQuest.onCanceled += SetQuestCanceled;

            activeQuests.Add(newQuest);

            newQuest.OnRegister();
            OnQuestRegistered?.Invoke(newQuest);
        }

        return newQuest;
    }

    public void ReceiveReport(string category, object target, int successCount)
    {
        ReceiveReport(activeQuests, category, target, successCount);
        ReceiveReport(activeAchievements, category, target, successCount);
    }

    public void ReceiveReport(Category category, TaskTarget target, int successCount)
        => ReceiveReport(category.CodeName, target.Value, successCount);

    private void ReceiveReport(List<Quest> quests, string category, object target, int successCount)
    {
        foreach (var quest in quests.ToArray())
            quest.ReceieveReport(category, target, successCount);
    }

    public bool ContainsInActiveQuests(Quest quest) => activeQuests.Any(x => x.CodeName == quest.CodeName);
    public bool ContainsInCompleteQuests(Quest quest) => completedQuests.Any(x => x.CodeName == quest.CodeName);
    public bool ContainsInActiveAchievement(Quest quest) => activeAchievements.Any(x => x.CodeName == quest.CodeName);
    public bool ContainsInCompletedAchievements(Quest quest) => completedAchievements.Any(x => x.CodeName == quest.CodeName);

    private void Save()
    {
        var root = new JObject();
        root.Add(kActiveQuestsSavePath, CreateSaveDatas(activeQuests));
        root.Add(kCompletedQuestsSavePath, CreateSaveDatas(completedQuests));
        root.Add(kActiveAchievementsSavePath, CreateSaveDatas(activeAchievements));
        root.Add(kCompletedAchievementsSavePath, CreateSaveDatas(completedAchievements));

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
            var quest = database.FindQuestBy(saveData.codeName);
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
