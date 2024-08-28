using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/TransitionCondition/Enemy_Move")]
public class EnemyMoveCondition : TransitionCondition
{
    public override bool IsConditionValid()
    {
        return Owner.TargetCompo.Targeting();
    }
}
