public struct QuestSaveData
{
    public int codeName;
    public QuestState state;
    public TaskSaveData[] taskSaveData;
}

public struct TaskSaveData
{
    public int currentSuccess;
}