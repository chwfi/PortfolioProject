using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/TransitionCondition/IsDead")]
public class IsDead : TransitionCondition
{
    public override bool IsConditionValid()
    {
        return Owner.HealthCompo.Dead;
    }
}
