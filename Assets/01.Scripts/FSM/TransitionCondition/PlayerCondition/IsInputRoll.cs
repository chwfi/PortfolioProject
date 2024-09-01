using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/TransitionCondition/Player/IsInputRoll")]
public class IsInputRoll : TransitionCondition
{
    public override bool IsConditionValid()
    {
        var skill = Owner.SkillManagerCompo.GetSkill(SkillTypeEnum.Roll);
        if (skill != null)
            return Owner.MoveCompo.InputReader.Roll;
        else
            return false;
    }
}
