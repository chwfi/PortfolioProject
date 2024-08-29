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

        if (_owner.GetConditionValid(ConditionTypeEnum.IsAttackEnd))
            _stateMachine.ChangeState(StateTypeEnum.Idle);
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
