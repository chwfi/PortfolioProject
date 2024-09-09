using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;
using System.IO;
using System.Linq;

public class  QuestSystem : MonoSingleton<QuestSystem>
{
    #region SavePath
    private const string kCompletedAchievementsSavePath = "completedAchievements.json";
    private const string kSaveFileName = "questData.json";
    private string SaveFilePath => Path.Combine(Application.persistentDataPath, kSaveFileName);
    #endregion

    #region Handlers
    public delegate void QuestCompletedHandler(Quest quest);
    public delegate void QuestCanceledHandler(Quest quest);
    public delegate void QuestRecieveHandler(object target, int successCount);
    public delegate void CheckCompleteHandler();
    public delegate void QuestUpdateUIHandler();
    #endregion

    private List<Quest> _activeQuests = new();
    private List<Quest> _completedQuests = new();

    [SerializeField] private QuestDatabase _questDatabase;
    public QuestDatabase QuestDatabase => _questDatabase;

    #region Events
    public event QuestCompletedHandler OnQuestCompleted;
    public event QuestCanceledHandler OnQuestCanceled;

    public event QuestRecieveHandler OnQuestRecieved;
    public event CheckCompleteHandler OnCheckCompleted;
    public event QuestUpdateUIHandler OnUpdateQuestUI;
    #endregion

    public bool IsFileExist => File.Exists(SaveFilePath);

    private void OnApplicationQuit() 
    {
        Save();
    }

    private void Start() 
    {
        Load();

        if (!IsFileExist)
        {
            foreach (var quest in QuestDatabase.Quests)
            {
                Register(quest);
            }
        }
    }

    public Quest Register(Quest quest)
    {
        var newQuest = quest.Clone();

        newQuest.OnCompleted += SetQuestCompleted;
        newQuest.OnCanceled += SetQuestCanceled;

        _activeQuests.Add(newQuest);

        newQuest.OnRegister();

        return newQuest;
    }

    public void Report(object target, int successCount)
    {
        OnQuestRecieved?.Invoke(target, successCount);
        OnUpdateQuestUI?.Invoke();
        OnCheckCompleted?.Invoke();
    }

    private void Save()
    {
        var root = new JObject
        {
            { "activeQuests", CreateSaveDatas(_activeQuests) },
            { "completedQuests", CreateSaveDatas(_completedQuests) },
        };

        File.WriteAllText(SaveFilePath, root.ToString());
        Debug.Log($"Data saved to {SaveFilePath}");
    }

    private bool Load()
    {
        if (File.Exists(SaveFilePath))
        {
            var jsonContent = File.ReadAllText(SaveFilePath);
            var root = JObject.Parse(jsonContent);

            LoadSaveDatas(root["activeQuests"], _questDatabase, LoadActiveQuest);
            LoadSaveDatas(root["completedQuests"], _questDatabase, LoadCompleteQuest);

            Debug.Log("Data loaded from JSON file.");
            return true;
        }
        else
        {
            Debug.Log("Save file not found.");
            return false;
        }
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

    private void LoadSaveDatas(JToken datasToken, QuestDatabase database, Action<QuestSaveData, Quest> onSuccess)
    {
        var datas = datasToken as JArray;
        foreach (var data in datas)
        {
            var saveData = data.ToObject<QuestSaveData>();
            var quest = database.FindQuestBy(saveData.codeName);
            onSuccess?.Invoke(saveData, quest);
        }
    }

    private void LoadActiveQuest(QuestSaveData saveData, Quest quest)
    {
        var newQuest = Register(quest);
        newQuest.LoadFrom(saveData);
        OnUpdateQuestUI?.Invoke();
    }

    private void LoadCompleteQuest(QuestSaveData saveData, Quest quest)
    {
        var newQuest = quest.Clone();
        newQuest.LoadFrom(saveData);
        newQuest.OnRegisterUI();
        OnUpdateQuestUI?.Invoke();

        _completedQuests.Add(newQuest);
    }


    #region CallBacks
    private void SetQuestCompleted(Quest quest)
    {
        if (_activeQuests.Contains(quest))
        {
            _activeQuests.Remove(quest);
            _completedQuests.Add(quest);

            OnQuestCompleted?.Invoke(quest);
        }
    }

    private void SetQuestCanceled(Quest quest)
    {
        _activeQuests.Remove(quest);

        OnQuestCanceled?.Invoke(quest);

        Destroy(quest, Time.deltaTime);
    }
    #endregion
}
