using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/TransitionCondition/IsTargetDetected")]
public class IsTargetDetected : TransitionCondition
{
    public override bool IsConditionValid()
    {
        return Owner.TargetCompo.Targeting();
    }
}
