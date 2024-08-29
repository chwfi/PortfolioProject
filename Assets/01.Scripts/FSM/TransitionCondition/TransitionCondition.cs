using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ConditionTypeEnum
{
    IsInputMove,
    IsInputIdle,
    IsInputAttack,
    None,
    IsTargetDetected,
    IsTargetNull,
    IsInputRoll,
}

public abstract class TransitionCondition : ScriptableObject
{
    [SerializeField] private string _description;
    public ConditionTypeEnum ConditionType;

    public Entity Owner { get; set; }

    public abstract bool IsConditionValid();    
}
