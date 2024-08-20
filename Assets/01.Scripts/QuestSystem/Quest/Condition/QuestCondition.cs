using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestCondition : ScriptableObject
{
    [SerializeField] private string _description;

    public abstract bool IsPrepared(Quest quest);
}
