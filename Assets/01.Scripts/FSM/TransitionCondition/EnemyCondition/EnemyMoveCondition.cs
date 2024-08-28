using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/TransitionCondition/Enemy_Move")]
public class EnemyMoveCondition : TransitionCondition
{
    private EnemyTarget _targetCompo => Owner.TargetCompo as EnemyTarget;

    public override bool IsConditionValid()
    {
        return _targetCompo.Targeting();
    }
}
