using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public IdleState(Entity owner, StateMachine stateMachine, string animName) : base(owner, stateMachine, animName)
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

        if (_owner.GetConditionValid(ConditionTypeEnum.IsInputMove))
            _stateMachine.ChangeState(StateTypeEnum.Move);     

        if (_owner.GetConditionValid(ConditionTypeEnum.IsTargetDetected))
            _stateMachine.ChangeState(StateTypeEnum.Move);

        if (_owner.GetConditionValid(ConditionTypeEnum.IsInputAttack))
            _stateMachine.ChangeState(StateTypeEnum.Attack);     
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
