using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/TransitionCondition/IsHit")]
public class IsHit : TransitionCondition
{
    public override bool IsConditionValid()
    {
        return Owner.HealthCompo.Hit;
    }
}
