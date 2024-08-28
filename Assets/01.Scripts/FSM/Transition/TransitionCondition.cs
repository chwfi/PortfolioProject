using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TransitionCondition : ScriptableObject
{
    public Entity Owner { get; set; }

    public abstract bool IsConditionValid();    
}
