using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public IdleState(Entity owner, StateMachine stateMachine, TransitionCondition condition, string animName) : base(owner, stateMachine, condition, animName)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        _owner.ConditionDictionary.TryGetValue(ConditionTypeEnum.IdleToMove, out _condition);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_condition.IsConditionValid())
            _stateMachine.ChangeState(StateTypeEnum.Move);          
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
