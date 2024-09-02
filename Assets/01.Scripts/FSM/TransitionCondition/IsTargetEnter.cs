using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/TransitionCondition/IsTargetEnter")]
public class IsTargetEnter : TransitionCondition
{
    public override bool IsConditionValid()
    {
        var skill = Owner.SkillManagerCompo.GetSkill(SkillTypeEnum.Attack);
        if (skill != null)
            return Owner.TargetCompo.IsTargetEnter();
        else
            return false;
    }
}
