using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ConditionTypeEnum
{
    IdleToMove,
    MoveToIdle,
}

public abstract class TransitionCondition : ScriptableObject
{
    [SerializeField] private string _description;
    public ConditionTypeEnum ConditionType;
    public Entity Owner { get; set; }

    public abstract bool IsConditionValid();    
}
