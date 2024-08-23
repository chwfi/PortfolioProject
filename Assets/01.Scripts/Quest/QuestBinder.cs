using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestBinder
{
    

    public Quest OwnerQuest;

    public void SetOwner(Quest quest)
    {
        OwnerQuest = quest;
    }

    public void NotifyUIUpdate()
    {
        //OnUIUpdate?.Invoke(this);
        
    }
}
