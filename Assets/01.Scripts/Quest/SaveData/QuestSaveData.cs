public struct QuestSaveData
{
    public string codeName;
    public QuestState state;
    public TaskSaveData[] taskSaveData;
}

public struct TaskSaveData
{
    public int currentSuccess;
}