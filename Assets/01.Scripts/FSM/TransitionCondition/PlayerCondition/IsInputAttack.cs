using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/TransitionCondition/Player/IsInputAttack")]
public class IsInputAttack : TransitionCondition
{
    public override bool IsConditionValid()
    {
        var skill = Owner.SkillManagerCompo.GetSkill(SkillTypeEnum.Attack);
        if (skill != null)
            return Owner.MoveCompo.InputReader.Attack;
        else
            return false;
    }
}
