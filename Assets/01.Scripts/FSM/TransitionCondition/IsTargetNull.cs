using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/TransitionCondition/IsTargetNull")]
public class IsTargetNull : TransitionCondition
{
    public override bool IsConditionValid()
    {
        if (!Owner.TargetCompo.Targeting())
            return true;
        else
            return false;
    }
}
