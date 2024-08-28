using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/TransitionCondition/Enemy_Stop")]
public class EnemyStopCondition : TransitionCondition
{
    private EnemyTarget _targetCompo => Owner.TargetCompo as EnemyTarget;
    
    public override bool IsConditionValid()
    {
        if (!_targetCompo.Targeting())
            return true;
        else
            return false;
    }
}
