using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    private TransitionCondition _condition;

    public MoveState(Entity owner, StateMachine stateMachine, string animName) : base(owner, stateMachine, animName)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        _owner.ConditionDictionary.TryGetValue(ConditionTypeEnum.MoveToIdle, out _condition);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        _owner.MoveCompo.OnUpdate();

        if (_condition.IsConditionValid())
            _stateMachine.ChangeState(StateTypeEnum.Idle);
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
