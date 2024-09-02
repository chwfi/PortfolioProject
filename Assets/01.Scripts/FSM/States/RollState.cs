using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollState : State
{
    public RollState(Entity owner, StateMachine stateMachine, string animName) : base(owner, stateMachine, animName)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        
        var skill = _owner.SkillManagerCompo.GetSkill(SkillTypeEnum.Roll);
        skill.PlaySkill();
    }

    public override void FixedUpdateState()
    {
        base.FixedUpdateState();
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
