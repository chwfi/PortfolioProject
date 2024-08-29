using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    public MoveState(Entity owner, StateMachine stateMachine, string animName) : base(owner, stateMachine, animName)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_owner.GetConditionValid(ConditionTypeEnum.IsInputIdle))
            _stateMachine.ChangeState(StateTypeEnum.Idle);

        if (_owner.GetConditionValid(ConditionTypeEnum.IsTargetNull))
            _stateMachine.ChangeState(StateTypeEnum.Idle);

        if (_owner.GetConditionValid(ConditionTypeEnum.IsInputAttack))
            _stateMachine.ChangeState(StateTypeEnum.Attack);

        if (_owner.GetConditionValid(ConditionTypeEnum.IsInputRoll))
            _stateMachine.ChangeState(StateTypeEnum.Roll);
    }

    public override void FixedUpdateState()
    {
        base.FixedUpdateState();

        _owner.MoveCompo.OnMove();
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
