using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/TransitionCondition/IsAttackEnd")]
public class IsAttackEnd : TransitionCondition
{
    public override bool IsConditionValid()
    {
        return Owner.AnimatorControllerCompo.IsAnimationEnd();
    }
}
