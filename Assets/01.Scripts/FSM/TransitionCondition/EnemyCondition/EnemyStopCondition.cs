using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/TransitionCondition/Enemy_Stop")]
public class EnemyStopCondition : TransitionCondition
{
    public override bool IsConditionValid()
    {
        if (!Owner.TargetCompo.Targeting())
            return true;
        else
            return false;
    }
}
