using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    public AttackState(Entity owner, StateMachine stateMachine, string animName) : base(owner, stateMachine, animName)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        _owner.MoveCompo.StopImmediately();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        _owner.IsConditionsValid(StateTypeEnum.Hit);
        _owner.IsConditionsValid(StateTypeEnum.Dead);
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
